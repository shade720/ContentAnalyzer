using Common.EntityFramework;
using DataCollectionServiceClient;
using Google.Protobuf.WellKnownTypes;

namespace DevTool.Models;

internal class CollectionServiceClient : ServiceClient, IDisposable
{
    private readonly DataCollection.DataCollectionClient _dataCollectionClient;

    public CollectionServiceClient(string dataCollectionServiceHost) : base(dataCollectionServiceHost)
    {
        _dataCollectionClient = new DataCollection.DataCollectionClient(Channel);
    }

    public override void StartService()
    {
        _dataCollectionClient.StartCollectionService(new StartCollectionServiceRequest());
    }

    public override void StopService()
    {
        _dataCollectionClient.StopCollectionService(new StopCollectionServiceRequest());
    }
    public override string GetLogFile(DateTime date)
    {
        var result = _dataCollectionClient.GetLogs(new LogRequest { LogDate = Timestamp.FromDateTime(date.ToUniversalTime()) });
        return result.LogFile.ToStringUtf8();
    }

    public List<Comment> GetComments(int startIndex)
    {
        var comments = _dataCollectionClient.GetCommentsFrom(new GetCommentsRequest { StartIndex = startIndex });
        return comments.Result.Select(comment => new Comment
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

    public void LoadConfiguration(string appsettingFile)
    {
        _dataCollectionClient.SetConfiguration(new SetConfigurationRequest {Settings = appsettingFile});
    }

    public new void Dispose()
    {
        Channel.Dispose();
    }
}