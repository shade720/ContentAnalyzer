using Common.SharedDomain;
using DataAnalysisService.Application;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Dynamic;

namespace DataAnalysisService.Grpc.UI.Services;

public class DataAnalysisAPI : DataAnalysis.DataAnalysisBase
{
    private readonly DataAnalyzer _dataAnalyzer;
    private readonly IEvaluatedCommentsRepository _evaluatedCommentsRepository;

    public DataAnalysisAPI(
        DataAnalyzer dataAnalyzer,
        IEvaluatedCommentsRepository evaluatedCommentsRepository)
    {
        _dataAnalyzer = dataAnalyzer;
        _evaluatedCommentsRepository = evaluatedCommentsRepository;
    }

    public override Task<StartAnalysisServiceReply> StartAnalysisService(StartAnalysisServiceRequest request, ServerCallContext context)
    {
        if (_dataAnalyzer.IsAnalysisStarted)
            return Task.FromResult(new StartAnalysisServiceReply());

        _dataAnalyzer.StartAnalysis();
        Log.Logger.Information("Service started");
        return Task.FromResult(new StartAnalysisServiceReply());
    }

    public override Task<StopAnalysisServiceReply> StopAnalysisService(StopAnalysisServiceRequest request, ServerCallContext context)
    {
        if (!_dataAnalyzer.IsAnalysisStarted)
            return Task.FromResult(new StopAnalysisServiceReply());

        _dataAnalyzer.StopAnalysis();
        Log.Logger.Information("Service stopped");
        return Task.FromResult(new StopAnalysisServiceReply());
    }

    public override async Task<EvaluatedCommentsReply> GetEvaluatedComments(EvaluatedCommentsRequest request, ServerCallContext context)
    {
        var filter = new CommentsQueryFilter
        {
            AuthorId = request.Filter.AuthorId,
            PostId = request.Filter.PostId,
            GroupId = request.Filter.GroupId,
            Text = request.Filter.Text,
            Category = request.Filter.Category,
            FromDate = request.Filter.FromDate.ToDateTime(),
            ToDate = request.Filter.ToDate.ToDateTime()
        };

        var range = (await _evaluatedCommentsRepository
            .GetRange(filter))
            .Select(evaluateResult => new EvaluatedCommentProto
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
            }).ToArray();

        return new EvaluatedCommentsReply { EvaluatedComments = { new RepeatedField<EvaluatedCommentProto> { range } } };
    }

    public override async Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        var logDate = request.LogDate.ToDateTime().ToLocalTime();

        var requiredFilePath = Directory.GetFiles("./Logs/").FirstOrDefault(x => x.Contains($"{logDate:yyyyMMdd}"));
        if (requiredFilePath is null)
        {
            Log.Logger.Error("Log file for {date} does not exist", logDate.ToString("yyyyMMdd"));
            throw new ArgumentException("Log file for {date} does not exist");
        }
        await using var fileStream = new FileStream(requiredFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return new LogReply { LogFile = await ByteString.FromStreamAsync(fileStream) };
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

        return new SetConfigurationReply();
    }
}