using Common.SharedDomain;
using DataAnalysisClient;
using Google.Protobuf.WellKnownTypes;

namespace DevTool.Models;

internal class AnalysisServiceClient : ServiceClient<EvaluatedComment>
{
    private readonly DataAnalysis.DataAnalysisClient _dataAnalysisClient;
    public AnalysisServiceClient(string dataAnalysisServiceHost) : base(dataAnalysisServiceHost)
    {
        _dataAnalysisClient = new DataAnalysis.DataAnalysisClient(Channel);
    }

    public override void ClearDatabase()
    {
        _dataAnalysisClient.ClearEvaluatedDatabaseAsync(new ClearEvaluatedDatabaseRequest());
    }

    public override IEnumerable<EvaluatedComment> GetResults(CommentsQueryFilter filter)
    {
        var requestFilter = new CommentsQueryFilterProto
        {
            AuthorId = filter.AuthorId,
            PostId = filter.PostId,
            GroupId = filter.GroupId,
            FromDate = new Timestamp { Seconds = new DateTimeOffset(filter.FromDate).ToUnixTimeSeconds() },
            ToDate = new Timestamp { Seconds = new DateTimeOffset(filter.ToDate).ToUnixTimeSeconds() }
        };
        var comments = _dataAnalysisClient.GetEvaluatedCommentsAsync(new EvaluatedCommentsRequest { Filter = requestFilter }).ResponseAsync.Result;
        return comments.EvaluatedComments.Select(evaluateResultProto => new EvaluatedComment
        {
            Id = evaluateResultProto.Id,
            RelatedComment = new Comment
            {
                Id = evaluateResultProto.Comment.Id,
                CommentId = evaluateResultProto.Comment.CommentId,
                AuthorId = evaluateResultProto.Comment.AuthorId,
                GroupId = evaluateResultProto.Comment.GroupId,
                PostDate = evaluateResultProto.Comment.PostDate.ToDateTime(),
                PostId = evaluateResultProto.Comment.PostId,
                Text = evaluateResultProto.Comment.Text
            },
            CommentId = evaluateResultProto.CommentId,
            EvaluateCategory = evaluateResultProto.EvaluateCategory,
            EvaluateProbability = evaluateResultProto.EvaluateProbability
        });
    }
    public override void LoadConfiguration(string settings)
    {
        _dataAnalysisClient.SetConfigurationAsync(new SetConfigurationRequest { Settings = settings });
    }
    public override void StartService()
    {
        _dataAnalysisClient.StartAnalysisServiceAsync(new StartAnalysisServiceRequest());
    }

    public override void StopService()
    {
        _dataAnalysisClient.StopAnalysisServiceAsync(new StopAnalysisServiceRequest());
    }

    protected override string GetLogFile(DateTime date)
    {
        var result = _dataAnalysisClient.GetLogsAsync(new LogRequest { LogDate = Timestamp.FromDateTime(date.ToUniversalTime()) }).ResponseAsync.Result;
        return result.LogFile.ToStringUtf8();
    }

    public new void Dispose()
    {
        Channel.Dispose();
    }
}