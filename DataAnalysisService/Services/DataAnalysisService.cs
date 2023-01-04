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
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataAnalysisService.Services;

public class DataAnalysisService : DataAnalysis.DataAnalysisBase
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private static DatabaseClient<EvaluatedComment> _targetDatabase;
    private static DatabaseObserver _sourceDatabase;
    private static readonly Stopwatch Stopwatch = new();
    private static bool IsRunning { get; set; } = false;

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
        foreach (var model in AnalyzeModels)
        {
            StartModel(model.Key);
        }
        Stopwatch.Start();
        IsRunning = true;
        Log.Logger.Information("Service started");
        return Task.FromResult(new StartAnalysisServiceReply());
    }

    public override Task<StopAnalysisServiceReply> StopAnalysisService(StopAnalysisServiceRequest request, ServerCallContext context)
    {
        foreach (var model in AnalyzeModels)
        {
            Stop(model.Key);
        }
        Stopwatch.Stop();
        Stopwatch.Reset();
        IsRunning = false;
        Log.Logger.Information("Service stopped");
        return Task.FromResult(new StopAnalysisServiceReply());
    }


    public override Task<EvaluatedCommentsReply> GetEvaluatedComments(EvaluatedCommentsRequest request, ServerCallContext context)
    {
        var filter = new CommentsQueryFilter
        {
            AuthorId = request.Filter.AuthorId,
            PostId = request.Filter.PostId,
            GroupId = request.Filter.GroupId,
            FromDate = request.Filter.FromDate.ToDateTime(),
            ToDate = request.Filter.ToDate.ToDateTime()
        };
        var range = _targetDatabase.GetRange(filter);

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
        return Task.FromResult(new EvaluatedCommentsReply { EvaluatedComments = { convertedRange } });
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

    public override Task<SetConfigurationReply> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Updating settings...");
        UpdateSettings(request.Settings);
        Log.Logger.Information("Settings file updated");

        if (!IsRunning) return Task.FromResult(new SetConfigurationReply());
        Log.Logger.Information("Restarting service...");
        foreach (var model in AnalyzeModels)
        {
            Stop(model.Key);
        }
        AnalyzeModels.Clear();
        var config = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.UserDomainName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        Startup.ConfigureService(config);
        foreach (var model in AnalyzeModels)
        {
            StartModel(model.Key);
        }
        Log.Logger.Information("Service restarted on new configuration");
        return Task.FromResult(new SetConfigurationReply());
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

    private static void UpdateSettings(string settings)
    {
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var appSettingsJson = File.ReadAllText(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic oldConfig = JsonConvert.DeserializeObject<ExpandoObject>(appSettingsJson, jsonSettings);
        dynamic newConfig = JsonConvert.DeserializeObject<ExpandoObject>(settings, jsonSettings);

        if (oldConfig is null) throw new FileLoadException("Cannot deserialize appsettings.json file");
        if (newConfig is null) throw new FileLoadException("Cannot deserialize new settings file");

        if (((IDictionary<string, object>)newConfig).ContainsKey("ScanCommentsDelay"))
            oldConfig.ScanCommentsDelay = newConfig.ScanCommentsDelay;
        if (((IDictionary<string, object>)newConfig).ContainsKey("ScanPostDelay"))
            oldConfig.ScanPostDelay = newConfig.ScanPostDelay;
        if (((IDictionary<string, object>)newConfig).ContainsKey("PostQueueSize"))
            oldConfig.PostQueueSize = newConfig.PostQueueSize;

        if (((IDictionary<string, object>)newConfig).ContainsKey("ApplicationId"))
            oldConfig.VkSettings.ApplicationId = newConfig.ApplicationId;
        if (((IDictionary<string, object>)newConfig).ContainsKey("SecureKey"))
            oldConfig.VkSettings.SecureKey = newConfig.SecureKey;
        if (((IDictionary<string, object>)newConfig).ContainsKey("ServiceAccessKey"))
            oldConfig.VkSettings.ServiceAccessKey = newConfig.ServiceAccessKey;
        if (((IDictionary<string, object>)newConfig).ContainsKey("Communities"))
            oldConfig.VkSettings.Communities = newConfig.Communities;

        var newAppSettingsJson = JsonConvert.SerializeObject(oldConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(appSettingsPath, newAppSettingsJson);
    }
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