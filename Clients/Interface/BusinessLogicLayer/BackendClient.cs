using Common.SharedDomain;
using ContentAnalyzer.Frontend.Desktop.Models;
using GatewayService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;

namespace ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;

public class BackendClient : IDisposable
{
    private readonly Metadata _authenticationMetadata;
    private readonly GrpcChannel _backendChannel;
    private readonly GatewayService.GatewayService.GatewayServiceClient _backendGatewayClient;

    public BackendClient(string backendHost, string login, string token)
    {
        _authenticationMetadata = new Metadata
        {
            {"Username", login},
            {"Password", token}
        };
        _backendChannel = GrpcChannel.ForAddress(backendHost);
        _backendGatewayClient = new GatewayService.GatewayService.GatewayServiceClient(_backendChannel);
    }

    public async Task StartDataCollectionServiceAsync()
    {
        await _backendGatewayClient.StartCollectionServiceAsync(new StartCollectionServiceRequest(), _authenticationMetadata);
    }

    public async Task StopDataCollectionServiceAsync()
    {
        await _backendGatewayClient.StopCollectionServiceAsync(new StopCollectionServiceRequest(), _authenticationMetadata);
    }

    public async Task StartDataAnalysisServiceAsync()
    {
        await _backendGatewayClient.StartAnalysisServiceAsync(new StartAnalysisServiceRequest(), _authenticationMetadata);
    }

    public async Task StopDataAnalysisServiceAsync()
    {
        await _backendGatewayClient.StopAnalysisServiceAsync(new StopAnalysisServiceRequest(), _authenticationMetadata);
    }

    public async Task LoadCollectionServiceConfiguration(CollectionServiceConfiguration configuration)
    {
        await _backendGatewayClient.SetCollectionServiceConfigurationAsync(new SetConfigurationRequest {Settings = JsonConvert.SerializeObject(configuration)}, _authenticationMetadata);
    }

    public async Task LoadAnalysisServiceConfiguration(AnalysisServiceConfiguration configuration)
    {
        await _backendGatewayClient.SetAnalysisServiceConfigurationAsync(new SetConfigurationRequest {Settings = JsonConvert.SerializeObject(configuration)}, _authenticationMetadata);
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(CommentsQueryFilter filter)
    {
        var comments = await _backendGatewayClient.GetCommentsAsync(new GetCommentsRequest
        {
            Filter = new CommentsQueryFilterProto
            {
                AuthorId = filter.AuthorId,
                PostId = filter.PostId,
                GroupId = filter.GroupId,
                Text = filter.Text,
                FromDate = new Timestamp { Seconds = new DateTimeOffset(filter.FromDate).ToUnixTimeSeconds() },
                ToDate = new Timestamp { Seconds = new DateTimeOffset(filter.ToDate).ToUnixTimeSeconds() }
            }
        }, _authenticationMetadata);
        return comments.CommentData.Select(comment => new Comment
        {
            Id = comment.Id,
            CommentId = comment.CommentId,
            AuthorId = comment.AuthorId,
            GroupId = comment.GroupId,
            PostDate = comment.PostDate.ToDateTime(),
            PostId = comment.PostId,
            Text = comment.Text
        });
    }

    public async Task<IEnumerable<EvaluatedComment>> GetEvaluatedCommentsAsync(CommentsQueryFilter filter)
    {
        var comments = await _backendGatewayClient.GetEvaluatedCommentsAsync(new EvaluatedCommentsRequest
        {
            Filter = new CommentsQueryFilterProto
            {
                AuthorId = filter.AuthorId,
                PostId = filter.PostId,
                GroupId = filter.GroupId,
                Text = filter.Text,
                Category = filter.Category,
                FromDate = new Timestamp { Seconds = new DateTimeOffset(filter.FromDate).ToUnixTimeSeconds() },
                ToDate = new Timestamp { Seconds = new DateTimeOffset(filter.ToDate).ToUnixTimeSeconds() }
            }
        }, _authenticationMetadata);

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

    public async Task<string> GetDataCollectionServiceLogsAsync(DateTime date)
    {
        var result = await _backendGatewayClient.GetCollectionServiceLogsAsync(new LogRequest { LogDate = date.ToUniversalTime().ToTimestamp() }, _authenticationMetadata);
        return result.LogFile.ToStringUtf8();
    }

    public async Task<string> GetDataAnalysisServiceLogsAsync(DateTime date)
    {
        var result = await _backendGatewayClient.GetAnalysisServiceLogsAsync(new LogRequest { LogDate = date.ToUniversalTime().ToTimestamp() }, _authenticationMetadata);
        return result.LogFile.ToStringUtf8();
    }

    public void Dispose()
    {
        _backendChannel.Dispose();
    }
}