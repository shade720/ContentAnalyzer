using Common.SharedDomain;
using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Dynamic;

namespace DataCollectionService.Grpc.UI.Services;

public class DataCollectionAPI : DataCollection.DataCollectionBase
{
    private readonly ICollector _collector;
    private readonly ICommentsRepository _repository;

    public DataCollectionAPI(ICollector collector, ICommentsRepository repository)
    {
        _collector = collector;
        _repository = repository;
    }

    public override async Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        _collector.OnCommentCollectedEvent += OnCommentCollected;
        _collector.StartCollecting();
        return new StartCollectionServiceReply();
    }

    private async Task OnCommentCollected(VkComment comment)
    {
        await _repository.Add(CommentsConverter.ConvertFromVkComment(comment));
    }

    public override async Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        _collector.OnCommentCollectedEvent -= OnCommentCollected;
        _collector.StopCollecting();
        return new StopCollectionServiceReply();
    }

    public override async Task<GetCommentsReply> GetComments(GetCommentsRequest request, ServerCallContext context)
    {
        var filter = new CommentsQueryFilter
        {
            AuthorId = request.Filter.AuthorId,
            PostId = request.Filter.PostId,
            GroupId = request.Filter.GroupId,
            FromDate = request.Filter.FromDate.ToDateTime(),
            ToDate = request.Filter.ToDate.ToDateTime()
        };
        var range = (await _repository
            .GetRange(filter))
            .Select(comment => new CommentDataProto
            {
                AuthorId = comment.AuthorId,
                CommentId = comment.CommentId,
                GroupId = comment.GroupId,
                PostDate = Timestamp.FromDateTime(comment.PostDate.ToUniversalTime()),
                PostId = comment.PostId,
                Text = comment.Text
            });
        return new GetCommentsReply { CommentData = { new RepeatedField<CommentDataProto> { range } } };
    }

    public override async Task<ClearCommentsDatabaseReply> ClearCommentsDatabase(ClearCommentsDatabaseRequest request, ServerCallContext context)
    {
        await _repository.Clear();
        return new ClearCommentsDatabaseReply();
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        var logDate = request.LogDate.ToDateTime().ToLocalTime();

        var requiredFilePath = Directory.GetFiles(@"./Logs/", $"log{logDate:yyyyMMdd}*.txt").SingleOrDefault();

        if (requiredFilePath is null)
        {
            Log.Logger.Error("Log file for {date} does not exist", logDate.ToString("yyyyMMdd"));
            return Task.FromResult(new LogReply());
        }

        using var fileStream = new FileStream(requiredFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        return Task.FromResult(new LogReply { LogFile = ByteString.FromStream(fileStream) });
    }

    public override async Task<SetConfigurationReply> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Updating settings...");

        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var appSettingsJson = await File.ReadAllTextAsync(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? oldConfig = JsonConvert.DeserializeObject<ExpandoObject>(appSettingsJson, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(request.Settings, jsonSettings);

        if (oldConfig is null)
        {
            Log.Logger.Error("Cannot deserialize appsettings.json file");
            throw new ApplicationException("Local config is corrupted or missing");
        }
        if (newConfig is null)
        {
            Log.Logger.Error("Cannot deserialize new settings file");
            throw new ApplicationException("Remote config is corrupted or missing");
        }

        var newConfigDict = (IDictionary<string, object>)newConfig;
        var oldConfigDict = (IDictionary<string, object>)oldConfig;

        foreach (var pair in newConfigDict)
        {
            if (oldConfigDict.ContainsKey(pair.Key))
                oldConfigDict[pair.Key] = pair.Value;
        }

        var newAppSettingsJson = JsonConvert.SerializeObject(oldConfig, Formatting.Indented, jsonSettings);
        await File.WriteAllTextAsync(appSettingsPath, newAppSettingsJson);

        Log.Logger.Information("Service restarted on new configuration");
        return new SetConfigurationReply();
    }
}