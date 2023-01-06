using System.Dynamic;
using Common;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Serilog;
using DataCollectionService.BusinessLogicLayer;

namespace DataCollectionService.Services;

public class DataCollectionAPI : DataCollection.DataCollectionBase
{
    private readonly DataCollector _dataCollector;

    public DataCollectionAPI(DataCollector dataCollector)
    {
        _dataCollector = dataCollector;
    }

    public override Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        _dataCollector.StartCollecting();
        return Task.FromResult(new StartCollectionServiceReply());
    }

    public override Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        _dataCollector.StopCollecting();
        return Task.FromResult(new StopCollectionServiceReply());
    }

    public override Task<GetCommentsReply> GetComments(GetCommentsRequest request, ServerCallContext context)
    {
        var filter = new CommentsQueryFilter
        {
            AuthorId = request.Filter.AuthorId,
            PostId = request.Filter.PostId,
            GroupId = request.Filter.GroupId,
            FromDate = request.Filter.FromDate.ToDateTime(),
            ToDate = request.Filter.ToDate.ToDateTime()
        };
        var range = _dataCollector
            .GetCollectedComments(filter).
            Select(comment => new CommentDataProto
            {
                Id = comment.Id,
                AuthorId = comment.AuthorId,
                CommentId = comment.CommentId,
                GroupId = comment.GroupId,
                PostDate = Timestamp.FromDateTime(comment.PostDate.ToUniversalTime()),
                PostId = comment.PostId,
                Text = comment.Text
            });
        return Task.FromResult(new GetCommentsReply {CommentData = { new RepeatedField<CommentDataProto> { range } } });
    }

    public override Task<ClearCommentsDatabaseReply> ClearCommentsDatabase(ClearCommentsDatabaseRequest request, ServerCallContext context)
    {
        _dataCollector.ClearCollectedComments();
        return Task.FromResult(new ClearCommentsDatabaseReply());
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Uptime: {0}", _dataCollector.CurrentWorkingTime);
        var logDate = request.LogDate.ToDateTime().ToLocalTime();
        var requiredFilePath = Directory.GetFiles(@"./Logs/", $"log{logDate:yyyyMMdd}*.txt").SingleOrDefault();
        if (string.IsNullOrEmpty(requiredFilePath)) return Task.FromResult(new LogReply());
        if (!File.Exists(requiredFilePath)) return Task.FromResult(new LogReply());
        using var fileStream = new FileStream(requiredFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return Task.FromResult(new LogReply { LogFile = ByteString.FromStream(fileStream) });
    }

    public override Task<SetConfigurationReply> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Updating settings...");
        UpdateSettings(request.Settings);
        Log.Logger.Information("Settings file updated");

        if (!_dataCollector.IsRunning) return Task.FromResult(new SetConfigurationReply());
        Log.Logger.Information("Restarting service...");
        
        _dataCollector.RestartCollecting();

        Log.Logger.Information("Service restarted on new configuration");
        return Task.FromResult(new SetConfigurationReply());
    }


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
}