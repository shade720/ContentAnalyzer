using Common;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Serilog;

namespace DataAnalysisService.Services;

public class DataAnalysisAPI : DataAnalysis.DataAnalysisBase
{
    private readonly BusinessLogicLayer.DataAnalyzer _dataAnalyzer;

    public DataAnalysisAPI(BusinessLogicLayer.DataAnalyzer dataAnalyzer)
    {
        _dataAnalyzer = dataAnalyzer;
    }

    public override Task<StartAnalysisServiceReply> StartAnalysisService(StartAnalysisServiceRequest request, ServerCallContext context)
    {
        if (!_dataAnalyzer.IsContainsModels)
            throw new ArgumentException($"At least one analysis model must be added {nameof(StartAnalysisService)}");
        _dataAnalyzer.StartService();
        Log.Logger.Information("Service started");
        return Task.FromResult(new StartAnalysisServiceReply());
    }

    public override Task<StopAnalysisServiceReply> StopAnalysisService(StopAnalysisServiceRequest request, ServerCallContext context)
    {
        _dataAnalyzer.StopService();
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

        var range = _dataAnalyzer
            .GetProcessedComments(filter)
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
            });

        return Task.FromResult(new EvaluatedCommentsReply { EvaluatedComments = { new RepeatedField<EvaluatedCommentProto> { range } } });
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Uptime: {0}", _dataAnalyzer.CurrentWorkingTime);

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

    public override Task<SetConfigurationReply> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Updating settings...");
        var success = _dataAnalyzer.UpdateSettings(request.Settings);
        if (!success)
            return Task.FromResult(new SetConfigurationReply());

        Log.Logger.Information("Settings file updated");

        if (!_dataAnalyzer.IsRunning)
            return Task.FromResult(new SetConfigurationReply());
        Log.Logger.Information("Restarting service...");

        _dataAnalyzer.Restart();

        Log.Logger.Information("Service restarted on new configuration");
        return Task.FromResult(new SetConfigurationReply());
    }
}