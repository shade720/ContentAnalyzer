using Common.EntityFramework;
using DataAnalysisServiceClient;
using Google.Protobuf.WellKnownTypes;

namespace DevTool.Models;

internal class AnalysisServiceClient : ServiceClient, IDisposable
{
    private readonly DataAnalysis.DataAnalysisClient _dataAnalysisClient;

    public override void StartService()
    {
        _dataAnalysisClient.StartAnalysisService(new StartAnalysisServiceRequest());
    }

    public override void StopService()
    {
        _dataAnalysisClient.StopAnalysisService(new StopAnalysisServiceRequest());
    }

    public override string GetLogFile(DateTime date)
    {
        var result = _dataAnalysisClient.GetLogs(new LogRequest { LogDate = Timestamp.FromDateTime(date.ToUniversalTime()) });
        return result.LogFile.ToStringUtf8();
    }

    public AnalysisServiceClient(string dataAnalysisServiceHost) : base(dataAnalysisServiceHost)
    {
        _dataAnalysisClient = new DataAnalysis.DataAnalysisClient(Channel);
    }

    public List<EvaluateResult> GetEvaluateResults(int startIndex)
    {
        var comments = _dataAnalysisClient.GetEvaluateResultsFrom(new EvaluateResultsRequest { StartIndex = startIndex });
        return comments.Result.Select(evaluateResultProto => new EvaluateResult
        {
            Id = evaluateResultProto.Id,
            CommentData = new CommentData
            {
                Id = evaluateResultProto.CommentData.Id,
                CommentId = evaluateResultProto.CommentData.CommentId,
                AuthorId = evaluateResultProto.CommentData.AuthorId,
                GroupId = evaluateResultProto.CommentData.GroupId,
                PostDate = evaluateResultProto.CommentData.PostDate.ToDateTime(),
                PostId = evaluateResultProto.CommentData.PostId,
                Text = evaluateResultProto.CommentData.Text
            },
            CommentDataId = evaluateResultProto.CommentId,
            EvaluateCategory = evaluateResultProto.EvaluateCategory,
            EvaluateProbability = evaluateResultProto.EvaluateProbability
        }).ToList();
    }

    public new void Dispose()
    {
        Channel.Dispose();
    }
}