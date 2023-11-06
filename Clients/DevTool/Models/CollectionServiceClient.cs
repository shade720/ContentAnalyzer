using Common.SharedDomain;
using DataCollectionClient;
using Google.Protobuf.WellKnownTypes;

namespace DevTool.Models;

internal class CollectionServiceClient : ServiceClient<Comment>
{
    private readonly DataCollection.DataCollectionClient _dataCollectionClient;

    public CollectionServiceClient(string dataCollectionServiceHost) 
        : base(dataCollectionServiceHost)
    {
        _dataCollectionClient = new DataCollection.DataCollectionClient(Channel);
    }

    public override void StartService()
    {
        _dataCollectionClient.StartCollectionServiceAsync(new StartCollectionServiceRequest());
    }

    public override void StopService()
    {
        _dataCollectionClient.StopCollectionServiceAsync(new StopCollectionServiceRequest());
    }

    public override void ClearDatabase()
    {
        _dataCollectionClient.ClearCommentsDatabaseAsync(new ClearCommentsDatabaseRequest());
    }

    protected override string GetLogFile(DateTime date)
    {
        var result = _dataCollectionClient.GetLogsAsync(new LogRequest { LogDate = Timestamp.FromDateTime(date.ToUniversalTime()) }).ResponseAsync.Result;
        return result.LogFile.ToStringUtf8();
    }
    public override void LoadConfiguration(string settings)
    {
        _dataCollectionClient.SetConfigurationAsync(new SetConfigurationRequest { Settings = settings });
    }

    public override IEnumerable<Comment> GetResults(CommentsQueryFilter filter)
    {
        var requestFilter = new CommentsQueryFilterProto
        {
            AuthorId = filter.AuthorId,
            PostId = filter.PostId,
            GroupId = filter.GroupId,
            FromDate = new Timestamp { Seconds = new DateTimeOffset(filter.FromDate).ToUnixTimeSeconds() },
            ToDate = new Timestamp { Seconds = new DateTimeOffset(filter.ToDate).ToUnixTimeSeconds() }
        };
        var comments = _dataCollectionClient.GetCommentsAsync(new GetCommentsRequest { Filter = requestFilter }).ResponseAsync.Result;
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