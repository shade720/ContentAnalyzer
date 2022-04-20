using Common;
using Common.EntityFramework;
using DataAnalysisService.AnalyzeModels.DomainClasses;
using DataAnalysisService.DatabaseClients;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAnalysisService.Services;

public class DataAnalysisService : DataAnalysis.DataAnalysisBase
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private readonly DatabaseClient<EvaluateResult> _targetDatabase;
    private readonly DatabaseObserver _sourceDatabase;
    
    #region PublicInterface

    public DataAnalysisService(IDbContextFactory<CommentsContext> contextFactory)
    {
        _sourceDatabase = new AllCommentsDb(contextFactory, 60000);
        _targetDatabase = new SuspiciousCommentsDb(contextFactory);
    }

    public override Task<StartAnalysisServiceReply> StartAnalysisService(StartAnalysisServiceRequest request, ServerCallContext context)
    {
        if (AnalyzeModels.Count == 0) throw new ArgumentException($"At least one analysis model must be added {nameof(StartAnalysisService)}");
        Log.Logger.Information("Service started, target database is ready");
        return Task.FromResult(new StartAnalysisServiceReply());
    }

    public override Task<StopAnalysisServiceReply> StopAnalysisService(StopAnalysisServiceRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Service stopped");
        return Task.FromResult(new StopAnalysisServiceReply());
    }

    public override Task<StartAllReply> StartAll(StartAllRequest request, ServerCallContext context)
    {
        foreach (var model in AnalyzeModels)
        {
            StartModel(model.Key);
        }
        return Task.FromResult(new StartAllReply());
    }

    public override Task<TrainAllReply> TrainAll(TrainAllRequest request, ServerCallContext context)
    {
        foreach (var model in AnalyzeModels)
        {
            TrainModel(model.Key);
        }
        return Task.FromResult(new TrainAllReply());
    }

    public override Task<StopAllReply> StopAll(StopAllRequest request, ServerCallContext context)
    {
        foreach (var model in AnalyzeModels)
        {
            Stop(model.Key);
        }
        Log.Logger.Information("All model are stopped");
        return Task.FromResult(new StopAllReply());
    }

    public override Task<StartModelReply> StartModel(StartModelRequest request, ServerCallContext context)
    {
        StartModel(request.ModelName);
        return Task.FromResult(new StartModelReply());
    }

    public override Task<TrainModelReply> TrainModel(TrainModelRequest request, ServerCallContext context)
    {
        TrainModel(request.ModelName);
        return Task.FromResult(new TrainModelReply());
    }

    public override Task<StopModelReply> StopModel(StopModelRequest request, ServerCallContext context)
    {
        Stop(request.ModelName);
        return Task.FromResult(new StopModelReply());
    }

    public override Task<EvaluateResultsReply> GetEvaluateResultsFrom(EvaluateResultsRequest request, ServerCallContext context)
    {
        var range = _targetDatabase.GetRange(request.StartIndex).Result;

        var convertedRange = new RepeatedField<EvaluateResultProto>();
        foreach (var evaluateResult in range)
        {
            convertedRange.Add(new EvaluateResultProto
            {
                Id = evaluateResult.Id,
                CommentId = evaluateResult.CommentDataId,
                CommentData = new CommentDataProto
                {
                    Id = evaluateResult.CommentData.Id,
                    AuthorId = evaluateResult.CommentData.AuthorId,
                    CommentId = evaluateResult.CommentData.CommentId,
                    GroupId = evaluateResult.CommentData.GroupId,
                    PostDate = Timestamp.FromDateTime(evaluateResult.CommentData.PostDate.ToUniversalTime()),
                    PostId = evaluateResult.CommentData.PostId,
                    Text = evaluateResult.CommentData.Text
                },
                EvaluateCategory = evaluateResult.EvaluateCategory,
                EvaluateProbability = evaluateResult.EvaluateProbability
            });
        }
        return Task.FromResult(new EvaluateResultsReply { Result = { convertedRange } });
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        var logDate = request.LogDate.ToDateTime().ToLocalTime();
        if (!File.Exists($@"./Logs/log{logDate:yyyyMMdd}.txt")) return Task.FromResult(new LogReply());
        using var fileStream = new FileStream($@"./Logs/log{logDate:yyyyMMdd}.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return Task.FromResult(new LogReply { LogFile = ByteString.FromStream(fileStream) });
    }

    #endregion

    #region Startup

    public static void AddModel(string modelName, Func<AnalyzeModel> modelConfiguration)
    {
        if (AnalyzeModels.ContainsKey(modelName))
        {
            Log.Logger.Error("Model {modelName} already added to service", modelName);
            return;
        }
        AnalyzeModels.Add(modelName, modelConfiguration.Invoke());
    }

    #endregion

    #region Private

    private void AnalyzeByAny(CommentData dataFrame)
    {
        foreach (var model in AnalyzeModels)
        {
            AnalyzeBy(model.Key, dataFrame);
        }
    }
    private void AnalyzeBy(string modelName, CommentData dataFrame)
    {
        if (!AnalyzeModels[modelName].IsRunning)
        {
            if (AnalyzeModels.All(model => !model.Value.IsRunning))
            {
                Log.Logger.Error("There are no running models. Stopping the work...");
                _sourceDatabase.StopLoading();
                return;
            }
            Log.Logger.Error("Model {modelName} not running", modelName);
            return;
        }
        try
        {
            AnalyzeModels[modelName].Predict(dataFrame);
        }
        catch (Exception e)
        {
            Log.Logger.Fatal("{message} {stackTrace}",e.Message, e.StackTrace);
            AnalyzeModels[modelName].StopModel();
        }
    }

    private void StartModel(string modelName)
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new ArgumentException($"Not all databases is registered {nameof(StartModel)}");
        if (AnalyzeModels[modelName].IsRunning)
        {
            Log.Logger.Error("Model {@modelName} already in work", modelName);
            return;
        }
        AnalyzeModels[modelName].Subscribe(null, InsertToTargetDb, error => ModelErrorHandler(error, modelName));
        try
        {
            AnalyzeModels[modelName].StartPredictiveModel();
            Log.Logger.Information("Model {@modelName} started listen predicts", modelName);
            EnsureLoading();
        }
        catch (Exception e)
        {
            Log.Logger.Fatal("{@message} {@stackTrace}", e.Message, e.StackTrace);
            AnalyzeModels[modelName].StopModel();
        }
    }

    private void ModelErrorHandler(string errorMessage, string modelName)
    {
        if (!errorMessage.Contains("NameError")) return;
        Log.Logger.Fatal("Model {@modelName} has stopped by script exception {@error}", modelName, errorMessage);
        AnalyzeModels[modelName].StopModel();
    }

    private void InsertToTargetDb(EvaluateResult evaluateResult)
    {
         _targetDatabase.Add(evaluateResult);
    }

    private void EnsureLoading()
    {
        if (_sourceDatabase.IsLoadingStarted) return;
        _sourceDatabase.OnDataArrived(AnalyzeByAny);
        _sourceDatabase.StartLoading();
        Log.Logger.Information("Source database started sending data");
    }

    private void TrainModel(string modelName)
    {
        if (AnalyzeModels[modelName].IsRunning)
        {
            Log.Logger.Error("Model {modelName} already in work", modelName);
            return;
        }
        try
        {
            AnalyzeModels[modelName].StartTrainModel();
            Log.Logger.Information("Model {modelName} started training", modelName);
        }
        catch (Exception e)
        {
            Log.Logger.Fatal("{message} {stackTrace}", e.Message, e.StackTrace);
            AnalyzeModels[modelName].StopModel();
        }
    }

    private void Stop(string modelName)
    {
        if (_sourceDatabase is null) throw new ArgumentException($"Source database is not registered {nameof(Stop)}");
        if (!AnalyzeModels[modelName].IsRunning)
        {
            Log.Logger.Error("Model {modelName} already stopped", modelName);
            return;
        }
        try
        {
            var isLastRunningModel = AnalyzeModels.Count(model => model.Value.IsRunning) == 1;
            if (_sourceDatabase.IsLoadingStarted && isLastRunningModel) _sourceDatabase.StopLoading();
            AnalyzeModels[modelName].Unsubscribe(null, InsertToTargetDb, null);
            AnalyzeModels[modelName].StopModel();
            Log.Logger.Information("{modelName} executing stopped", modelName);
        }
        catch (Exception e)
        {
            Log.Logger.Fatal("{0} {1}", e.Message, e.StackTrace);
        }
    }

    #endregion
}