using ContentAnalyzer.Gateway.EFCoreIdentity;
using ContentAnalyzer.Gateway.GrpcClients;
using GatewayService;
using Grpc.Core;
using System.Data;
using ClearEvaluatedDatabaseReply = GatewayService.ClearEvaluatedDatabaseReply;
using ClearEvaluatedDatabaseRequest = GatewayService.ClearEvaluatedDatabaseRequest;
using CommentProto = GatewayService.CommentProto;
using EvaluatedCommentProto = GatewayService.EvaluatedCommentProto;
using EvaluatedCommentsReply = GatewayService.EvaluatedCommentsReply;
using EvaluatedCommentsRequest = GatewayService.EvaluatedCommentsRequest;
using LogReply = GatewayService.LogReply;
using LogRequest = GatewayService.LogRequest;
using SetConfigurationReply = GatewayService.SetConfigurationReply;
using SetConfigurationRequest = GatewayService.SetConfigurationRequest;
using StartAnalysisServiceReply = GatewayService.StartAnalysisServiceReply;
using StartAnalysisServiceRequest = GatewayService.StartAnalysisServiceRequest;
using StopAnalysisServiceReply = GatewayService.StopAnalysisServiceReply;
using StopAnalysisServiceRequest = GatewayService.StopAnalysisServiceRequest;

namespace ContentAnalyzer.Gateway.Services;

public class ContentAnalyzerGateway : GatewayService.GatewayService.GatewayServiceBase
{
    private readonly TokenManager _tokenManager;
    private readonly DataCollectionServiceClient _dataCollectionService;
    private readonly DataAnalysisServiceClient _dataAnalysisService;

    public ContentAnalyzerGateway(
        TokenManager tokenManager,
        DataCollectionServiceClient dataCollectionService, 
        DataAnalysisServiceClient dataAnalysisService)
    {
        _tokenManager = tokenManager;
        _dataCollectionService = dataCollectionService;
        _dataAnalysisService = dataAnalysisService;
    }

    public override async Task<AddUserReply> AddUser(AddUserRequest request, ServerCallContext context)
    {
        var token = new Token {UserName = request.UserName, TokenData = request.Token};
        var result1 = await _tokenManager.SetToken(token);
        if (result1.Succeeded)
            return new AddUserReply();
        throw new DataException($"User not added!\r\n");
    }

    public override async Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        await _dataCollectionService.StartCollectionServiceAsync(new DataCollectionService.StartCollectionServiceRequest());
        return new StartCollectionServiceReply();
    }

    public override async Task<StartAnalysisServiceReply> StartAnalysisService(StartAnalysisServiceRequest request, ServerCallContext context)
    {
        await _dataAnalysisService.StartAnalysisServiceAsync(new DataAnalysisService.StartAnalysisServiceRequest());
        return new StartAnalysisServiceReply();
    }

    public override async Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        await _dataCollectionService.StopCollectionServiceAsync(new DataCollectionService.StopCollectionServiceRequest());
        return new StopCollectionServiceReply();
    }

    public override async Task<StopAnalysisServiceReply> StopAnalysisService(StopAnalysisServiceRequest request, ServerCallContext context)
    {
        await _dataAnalysisService.StopAnalysisServiceAsync(new DataAnalysisService.StopAnalysisServiceRequest());
        return new StopAnalysisServiceReply();
    }

    public override async Task<GetCommentsReply> GetComments(GetCommentsRequest request, ServerCallContext context)
    {
        var comments = await _dataCollectionService.GetComments(new DataCollectionService.GetCommentsRequest
        {
            Filter = new DataCollectionService.CommentsQueryFilterProto
            {
                PostId = request.Filter.PostId,
                GroupId = request.Filter.GroupId,
                AuthorId = request.Filter.AuthorId,
                Text = request.Filter.Text,
                FromDate = request.Filter.FromDate,
                ToDate = request.Filter.ToDate
            }
        });
        return new GetCommentsReply
        {
            CommentData =
            {
                comments.CommentData.Select(x => new CommentDataProto
                {
                    Id = x.Id,
                    CommentId = x.CommentId,
                    PostId = x.PostId,
                    GroupId = x.GroupId,
                    AuthorId = x.AuthorId,
                    Text = x.Text,
                    PostDate = x.PostDate
                })
            }
        };
    }

    public override async Task<EvaluatedCommentsReply> GetEvaluatedComments(EvaluatedCommentsRequest request, ServerCallContext context)
    {
        var evaluatedComments = await _dataAnalysisService.GetEvaluatedComments(new DataAnalysisService.EvaluatedCommentsRequest
        {
            Filter = new DataAnalysisService.CommentsQueryFilterProto
            {
                PostId = request.Filter.PostId,
                GroupId = request.Filter.GroupId,
                AuthorId = request.Filter.AuthorId,
                Text = request.Filter.Text,
                Category = request.Filter.Category,
                FromDate = request.Filter.FromDate,
                ToDate = request.Filter.ToDate
            }
        });
        return new EvaluatedCommentsReply
        {
            EvaluatedComments =
            {
                evaluatedComments.EvaluatedComments.Select(x => new EvaluatedCommentProto
                {
                    Id = x.Id,
                    CommentId = x.CommentId,
                    Comment = new CommentProto
                    {
                        Id = x.Comment.Id,
                        CommentId = x.Comment.CommentId,
                        PostId = x.Comment.PostId,
                        GroupId = x.Comment.GroupId,
                        AuthorId = x.Comment.AuthorId,
                        Text = x.Comment.Text,
                        PostDate = x.Comment.PostDate
                    },
                    EvaluateCategory = x.EvaluateCategory,
                    EvaluateProbability = x.EvaluateProbability
                })
            }
        };
    }

    public override async Task<ClearCommentsDatabaseReply> ClearCommentsDatabase(ClearCommentsDatabaseRequest request, ServerCallContext context)
    {
        await _dataCollectionService.ClearCommentsDatabase(new DataCollectionService.ClearCommentsDatabaseRequest());
        return new ClearCommentsDatabaseReply();
    }

    public override async Task<ClearEvaluatedDatabaseReply> ClearEvaluatedDatabase(ClearEvaluatedDatabaseRequest request, ServerCallContext context)
    {
        await _dataAnalysisService.ClearEvaluatedCommentsDatabase(new DataAnalysisService.ClearEvaluatedDatabaseRequest());
        return new ClearEvaluatedDatabaseReply();
    }

    public override async Task<SetConfigurationReply> SetCollectionServiceConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        await _dataCollectionService.SetCollectionServiceConfiguration(new DataCollectionService.SetConfigurationRequest { Settings = request.Settings});
        return new SetConfigurationReply();
    }

    public override async Task<SetConfigurationReply> SetAnalysisServiceConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        await _dataAnalysisService.SetAnalysisServiceConfiguration(new DataAnalysisService.SetConfigurationRequest {Settings = request.Settings});
        return new SetConfigurationReply();
    }

    public override async Task<LogReply> GetCollectionServiceLogs(LogRequest request, ServerCallContext context)
    {
        var logfile = await _dataCollectionService.GetCollectionServiceLogs(new DataCollectionService.LogRequest { LogDate = request.LogDate });
        return new LogReply { LogFile = logfile.LogFile };
    }

    public override async Task<LogReply> GetAnalysisServiceLogs(LogRequest request, ServerCallContext context)
    {
        var logfile = await _dataAnalysisService.GetAnalysisServiceLogs(new DataAnalysisService.LogRequest { LogDate = request.LogDate });
        return new LogReply { LogFile = logfile.LogFile };
    }
}