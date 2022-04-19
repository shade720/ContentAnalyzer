using Common;
using Common.EntityFramework;
using DataCollectionService.DatabaseClients;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.Services;

public class DataCollectionService : DataCollection.DataCollectionBase
{
    private static readonly List<IDataCollector> DataCollectors = new();
    private static DatabaseClient<CommentData>? _saveDatabase;

    #region PublicInterface

    public DataCollectionService(IDbContextFactory<CommentsContext> contextFactory)
    {
        _saveDatabase = new AllCommentsDb(contextFactory);
    }

    public override Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        _saveDatabase.Clear();
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StartCollecting();
        }
        return Task.FromResult(new StartCollectionServiceReply());
    }

    public override Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StopCollecting();
        }
        //_saveDatabase.Disconnect();
        return Task.FromResult(new StopCollectionServiceReply());
    }

    public override Task<GetCommentsReply> GetCommentsFrom(GetCommentsRequest request, ServerCallContext context)
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        var range = _saveDatabase.GetRange(request.StartIndex).Result;
        var convertedRange = new RepeatedField<CommentDataProto>();
        foreach (var comment in range)
        {
            convertedRange.Add(new CommentDataProto
            {
                Id = comment.Id,
                AuthorId = comment.AuthorId,
                CommentId = comment.CommentId,
                GroupId = comment.GroupId,
                PostDate = Timestamp.FromDateTime(comment.PostDate.ToUniversalTime()),
                PostId = comment.PostId,
                Text = comment.Text
            });
        }

        return Task.FromResult(new GetCommentsReply {CommentData = { convertedRange } });
    }

    #endregion

    #region Startup

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        if (_saveDatabase is null) throw new NullReferenceException("Save database are not registered");
        DataCollectors.Add(dataCollectorConfiguration.Invoke());
        DataCollectors[^1].Subscribe(dataFrame => _saveDatabase.Add(dataFrame));
    }
    #endregion
}