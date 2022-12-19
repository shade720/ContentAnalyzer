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
using System.Diagnostics;

namespace DataAnalysisService.Services;

public class DataAnalysisService : DataAnalysis.DataAnalysisBase
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private static DatabaseClient<EvaluatedComment> _targetDatabase;
    private static DatabaseObserver _sourceDatabase;
    private static readonly Stopwatch Stopwatch = new();

    public static int ObserveDelayMs { get; set; }

    #region PublicInterface

    public DataAnalysisService(IDbContextFactory<CommentsContext> contextFactory)
    {
        _sourceDatabase = new CommentsDatabaseObserver(contextFactory, ObserveDelayMs);
        _targetDatabase = new EvaluatedCommentsDatabaseClient(contextFactory);
    }

    public override Task<StartAnalysisServiceReply> StartAnalysisService(StartAnalysisServiceRequest request, ServerCallContext context)
    {
        if (AnalyzeModels.Count == 0) throw new ArgumentException($"At least one analysis model must be added {nameof(StartAnalysisService)}");
        Stopwatch.Start();
        foreach (var model in AnalyzeModels)
        {
            StartModel(model.Key);
        }
        Log.Logger.Information("Service started");
        return Task.FromResult(new StartAnalysisServiceReply());
    }

    public override Task<StopAnalysisServiceReply> StopAnalysisService(StopAnalysisServiceRequest request, ServerCallContext context)
    {
        Stopwatch.Stop();
        foreach (var model in AnalyzeModels)
        {
            Stop(model.Key);
        }
        Log.Logger.Information("Service stopped");
        return Task.FromResult(new StopAnalysisServiceReply());
    }


    public override Task<EvaluatedCommentsReply> GetEvaluatedComments(EvaluatedCommentsRequest request, ServerCallContext context)
    {
        var range = _targetDatabase.GetRange(request.StartIndex);

        var convertedRange = new RepeatedField<EvaluatedCommentProto>();
        foreach (var evaluateResult in range)
        {
            convertedRange.Add(new EvaluatedCommentProto
            {
                Id = evaluateResult.Id,
                CommentId = evaluateResult.CommentId,
                Comment = new CommentProto
                {
                    Id = evaluateResult.RelatedComment.Id,
                    AuthorId = evaluateResult.RelatedComment.AuthorId,
                    CommentId = evaluateResult.RelatedComment.CommentId,
                    GroupId = evaluateResult.RelatedComment.GroupId,
                    PostDate = Timestamp.FromDateTime(evaluateResult.RelatedComment.PostDate.ToUniversalTime()),
                    PostId = evaluateResult.RelatedComment.PostId,
                    Text = evaluateResult.RelatedComment.Text
                },
                EvaluateCategory = evaluateResult.EvaluateCategory,
                EvaluateProbability = evaluateResult.EvaluateProbability
            });
        }
        return Task.FromResult(new EvaluatedCommentsReply { Result = { convertedRange } });
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Uptime: {0}", Stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        var logDate = request.LogDate.ToDateTime().ToLocalTime();
        var requiredFilePath = Directory.GetFiles(@"./Logs/", $"log{logDate:yyyyMMdd}*.txt").SingleOrDefault();
        if (string.IsNullOrEmpty(requiredFilePath)) return Task.FromResult(new LogReply());
        if (!File.Exists(requiredFilePath)) return Task.FromResult(new LogReply());
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

    private void AnalyzeByAny(Comment frame)
    {
        foreach (var model in AnalyzeModels)
        {
            AnalyzeBy(model.Key, frame);
        }
    }
    private void AnalyzeBy(string modelName, Comment frame)
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
            AnalyzeModels[modelName].Predict(frame);
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

    private void InsertToTargetDb(EvaluatedComment evaluatedComment)
    {
         _targetDatabase.Add(evaluatedComment);
    }

    private void EnsureLoading()
    {
        if (_sourceDatabase.IsLoadingStarted) return;
        _sourceDatabase.OnDataArrived(AnalyzeByAny);
        _sourceDatabase.StartLoading();
        Log.Logger.Information("Source database started sending data");
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