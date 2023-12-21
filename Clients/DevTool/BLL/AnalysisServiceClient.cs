using Common.SharedDomain;
using DataAnalysisClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace DevTool.BLL
{
    public class AnalysisServiceClient
    {
        private readonly DataAnalysis.DataAnalysisClient _dataAnalysisClient;

        public AnalysisServiceClient(string dataAnalysisServiceHost)
        {
            _dataAnalysisClient = new DataAnalysis.DataAnalysisClient(GrpcChannel.ForAddress(dataAnalysisServiceHost));
        }

        public async Task StartService()
        {
            await _dataAnalysisClient.StartAnalysisServiceAsync(new StartAnalysisServiceRequest());
        }

        public async Task StopService()
        {
            await _dataAnalysisClient.StopAnalysisServiceAsync(new StopAnalysisServiceRequest());
        }

        public async Task LoadConfiguration(string settings)
        {
            await _dataAnalysisClient.SetConfigurationAsync(new SetConfigurationRequest { Settings = settings });
        }

        public async Task ClearDatabase()
        {
            await _dataAnalysisClient.ClearEvaluatedDatabaseAsync(new ClearEvaluatedDatabaseRequest());
        }

        public async Task<string> GetLogFile(DateTime date)
        {
            var result = await _dataAnalysisClient.GetLogsAsync(new LogRequest { LogDate = Timestamp.FromDateTime(date.ToUniversalTime()) });
            return result.LogFile.ToStringUtf8();
        }

        public async Task<IEnumerable<EvaluatedComment>> GetResults(CommentsQueryFilter filter)
        {
            var requestFilter = new CommentsQueryFilterProto
            {
                AuthorId = filter.AuthorId,
                PostId = filter.PostId,
                GroupId = filter.GroupId,
                FromDate = new Timestamp { Seconds = new DateTimeOffset(filter.FromDate).ToUnixTimeSeconds() },
                ToDate = new Timestamp { Seconds = new DateTimeOffset(filter.ToDate).ToUnixTimeSeconds() }
            };
            var comments = await _dataAnalysisClient.GetEvaluatedCommentsAsync(new EvaluatedCommentsRequest { Filter = requestFilter });
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
    }
}