using Common.EntityFramework;
using DataAnalysisServiceClient;
using DataCollectionServiceClient;
using Grpc.Net.Client;
namespace Interface;

public class Client
{
    private readonly GrpcChannel _analysisServiceChannel;
    private readonly GrpcChannel _collectionServiceChannel;
    private readonly DataCollection.DataCollectionClient _dataCollectionClient;
    private readonly DataAnalysis.DataAnalysisClient _dataAnalysisClient;

    public Client(string dataAnalysisServiceHost, string dataCollectionServiceHost)
    {
        _analysisServiceChannel = GrpcChannel.ForAddress(dataAnalysisServiceHost);
        _collectionServiceChannel = GrpcChannel.ForAddress(dataCollectionServiceHost);
        _dataCollectionClient = new DataCollection.DataCollectionClient(_collectionServiceChannel);
        _dataAnalysisClient = new DataAnalysis.DataAnalysisClient(_analysisServiceChannel);
    }

    public void StartDataCollectionService()
    {
        _dataCollectionClient.StartCollectionService(new StartCollectionServiceRequest());
    }

    public void StopDataCollectionService()
    {
        _dataCollectionClient.StopCollectionService(new StopCollectionServiceRequest());
    }

    public List<CommentData> GetComments(int startIndex)
    {
        var comments = _dataCollectionClient.GetCommentsFrom(new GetCommentsRequest {StartIndex = startIndex});
        return comments.Result.Select(comment => new CommentData
            {
                Id = comment.Id,
                CommentId = comment.CommentId,
                AuthorId = comment.AuthorId,
                GroupId = comment.GroupId,
                PostDate = comment.PostDate.ToDateTime(),
                PostId = comment.PostId,
                Text = comment.Text
            })
            .ToList();
    }

    public void StartDataAnalysisService()
    {
        _dataAnalysisClient.StartAnalysisService(new StartAnalysisServiceRequest());
    }

    public void StopDataAnalysisService()
    {
        _dataAnalysisClient.StopAnalysisService(new StopAnalysisServiceRequest());
    }

    public void StartAllAnalyzeModels()
    {
        _dataAnalysisClient.StartAll(new StartAllRequest());
    }

    public void StopAllModels()
    {
        _dataAnalysisClient.StopAll(new StopAllRequest());
    }

    public List<EvaluateResult> GetEvaluateResults(int startIndex)
    {
        var comments = _dataAnalysisClient.GetEvaluateResultsFrom(new EvaluateResultsRequest { StartIndex = startIndex});
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

    public void Dispose()
    {
        _analysisServiceChannel.Dispose();
        _collectionServiceChannel.Dispose();
    }
}