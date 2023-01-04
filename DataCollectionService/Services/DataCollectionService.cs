using System.Diagnostics;
using System.Dynamic;
using Common;
using Common.EntityFramework;
using DataCollectionService.DatabaseClients;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Serilog;
using Azure.Core;

namespace DataCollectionService.Services;

public class DataCollectionService : DataCollection.DataCollectionBase
{
    private static readonly List<IDataCollector> DataCollectors = new();
    private static DatabaseClient<Comment> _saveDatabase;
    private static readonly Stopwatch Stopwatch = new();
    private static bool IsRunning { get; set; } = false;

    #region PublicInterface

    public DataCollectionService(IDbContextFactory<CommentsContext> contextFactory)
    {
        _saveDatabase = new CommentsDatabaseClient(contextFactory);
    }

    public override Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        StartCollecting();
        Stopwatch.Start();
        IsRunning = true;
        return Task.FromResult(new StartCollectionServiceReply());
    }

    public override Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        StopCollecting();
        Stopwatch.Stop();
        Stopwatch.Reset();
        IsRunning = false;
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
        var range = _saveDatabase.GetRange(filter);
        var convertedRange = new RepeatedField<CommentDataProto>();
        foreach (var comment in range)
        {
            convertedRange.Add(new CommentDataProto
            {
                Id = comment.Id,
                AuthorId = comment.AuthorId,
                CommentId = comment.CommentId,
                GroupId = comment.GroupId,
                PostDate = Timestamp.FromDateTime(comment.PostDate.ToUniversalTime()),
                PostId = comment.PostId,
                Text = comment.Text
            });
        }

        return Task.FromResult(new GetCommentsReply {CommentData = { convertedRange } });
    }

    public override Task<ClearCommentsDatabaseReply> ClearCommentsDatabase(ClearCommentsDatabaseRequest request, ServerCallContext context)
    {
        _saveDatabase.Clear();
        return Task.FromResult(new ClearCommentsDatabaseReply());
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Uptime: {0}", Stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
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

        if (!IsRunning) return Task.FromResult(new SetConfigurationReply());
        Log.Logger.Information("Restarting service...");
        StopCollecting();
        DataCollectors.Clear();
        var config = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.UserDomainName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        Startup.ConfigureService(config);
        StartCollecting();
        Log.Logger.Information("Service restarted on new configuration");
        return Task.FromResult(new SetConfigurationReply());
    }

    #endregion

    #region Private

    private void StartCollecting()
    {
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StartCollecting();
            dataCollector.Subscribe(InsertToDatabase);
        }
    }

    private void StopCollecting()
    {
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StopCollecting();
            dataCollector.Unsubscribe(InsertToDatabase);
        }
    }

    private static void InsertToDatabase(Comment frame)
    {
        _saveDatabase.Add(frame);
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

    #endregion

    #region Startup

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        DataCollectors.Add(dataCollectorConfiguration.Invoke());
    }
    #endregion
}