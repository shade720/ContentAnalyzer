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

namespace DataCollectionService.Services;

public class DataCollectionService : DataCollection.DataCollectionBase
{
    private static readonly List<IDataCollector> DataCollectors = new();
    private readonly DatabaseClient<CommentData> _saveDatabase;
    private static readonly Stopwatch Stopwatch = new();

    #region PublicInterface

    public DataCollectionService(IDbContextFactory<CommentsContext> contextFactory)
    {
        _saveDatabase = new AllCommentsDb(contextFactory);
    }

    public override Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        _saveDatabase.Clear();
        StartCollecting();
        Stopwatch.Start();
        return Task.FromResult(new StartCollectionServiceReply());
    }

    public override Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        StopCollecting();
        Stopwatch.Stop();
        Stopwatch.Reset();
        return Task.FromResult(new StopCollectionServiceReply());
    }

    public override Task<GetCommentsReply> GetCommentsFrom(GetCommentsRequest request, ServerCallContext context)
    {
        var range = _saveDatabase.GetRange(request.StartIndex).Result;
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
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var appSettingsJson = File.ReadAllText(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic oldConfig = JsonConvert.DeserializeObject<ExpandoObject>(appSettingsJson, jsonSettings);
        dynamic newConfig = JsonConvert.DeserializeObject<ExpandoObject>(request.Settings, jsonSettings);

        oldConfig.ScanCommentsDelay = newConfig.ScanCommentsDelay;
        oldConfig.ScanPostDelay = newConfig.ScanPostDelay;
        oldConfig.PostQueueSize = newConfig.PostQueueSize;

        var newAppSettingsJso = JsonConvert.SerializeObject(oldConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(appSettingsPath, newAppSettingsJso);
        Log.Logger.Information("Settings file updated");
        Log.Logger.Information("Restarting service...");

        StopCollecting();
        DataCollectors.Clear();
        
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.UserDomainName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        Startup.ConfigureService(configBuilder.Build());
        StartCollecting();
        Log.Logger.Information("Service restarted on new configuration");
        return Task.FromResult(new SetConfigurationReply());
    }

    #endregion

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

    private void InsertToDatabase(CommentData dataFrame)
    {
        _saveDatabase.Add(dataFrame);
    }

    #region Startup

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        DataCollectors.Add(dataCollectorConfiguration.Invoke());
    }
    #endregion
}