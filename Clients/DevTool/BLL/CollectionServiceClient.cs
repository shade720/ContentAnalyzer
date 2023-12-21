using Common.SharedDomain;
using DataCollectionClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace DevTool.BLL
{
    public class CollectionServiceClient
    {
        private readonly DataCollection.DataCollectionClient _dataCollectionClient;

        public CollectionServiceClient(string dataCollectionServiceHost)
        {
            _dataCollectionClient = new DataCollection.DataCollectionClient(GrpcChannel.ForAddress(dataCollectionServiceHost));
        }

        public async Task StartService()
        {
            await _dataCollectionClient.StartCollectionServiceAsync(new StartCollectionServiceRequest());
        }

        public async Task StopService()
        {
            await _dataCollectionClient.StopCollectionServiceAsync(new StopCollectionServiceRequest());
        }

        public async Task ClearDatabase()
        {
            await _dataCollectionClient.ClearCommentsDatabaseAsync(new ClearCommentsDatabaseRequest());
        }

        public async Task<string> GetLogFile(DateTime fromTime)
        {
            var result = await _dataCollectionClient.GetLogsAsync(new LogRequest { LogDate = Timestamp.FromDateTime(fromTime.ToUniversalTime()) });
            return result.LogFile.ToStringUtf8();
        }

        public async Task LoadConfiguration(string settings)
        {
            await _dataCollectionClient.SetConfigurationAsync(new SetConfigurationRequest { Settings = settings });
        }

        public async Task<IEnumerable<Comment>> GetResults(CommentsQueryFilter filter)
        {
            var requestFilter = new CommentsQueryFilterProto
            {
                AuthorId = filter.AuthorId,
                PostId = filter.PostId,
                GroupId = filter.GroupId,
                FromDate = new Timestamp { Seconds = new DateTimeOffset(filter.FromDate).ToUnixTimeSeconds() },
                ToDate = new Timestamp { Seconds = new DateTimeOffset(filter.ToDate).ToUnixTimeSeconds() }
            };
            var comments = await _dataCollectionClient.GetCommentsAsync(new GetCommentsRequest { Filter = requestFilter });
            return comments.CommentData.Select(comment => new Comment
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
    }
}