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
    private static DatabaseClient<CommentData> _saveDatabase;

    #region PublicInterface

    public override Task<StartServiceReply> StartService(StartServiceRequest request, ServerCallContext context)
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        _saveDatabase.Connect();
        _saveDatabase.Clear();
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StartCollecting();
        }
        return Task.FromResult(new StartServiceReply());
    }

    public override Task<StopServiceReply> StopService(StopServiceRequest request, ServerCallContext context)
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StopCollecting();
        }
        _saveDatabase.Disconnect();
        return Task.FromResult(new StopServiceReply());
    }

    public override Task<GetCommentsReply> GetCommentsFrom(GetCommentsRequest request, ServerCallContext context)
    {
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
                PostDate = Timestamp.FromDateTime(comment.PostDate),
                PostId = comment.PostId,
                Text = comment.Text
            });
        }
        return Task.FromResult(new GetCommentsReply {Result = {convertedRange}});
    }

    #endregion

    #region Startup

    public static void SetDatabaseContextOption(DbContextOptions<CommentsContext> options)
    {
        _saveDatabase = new AllCommentsDb(options);
    }

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        if (_saveDatabase is null) throw new NullReferenceException("Save database are not registered");
        DataCollectors.Add(dataCollectorConfiguration.Invoke());
        DataCollectors[^1].Subscribe(dataFrame => _saveDatabase.Add(dataFrame));
    }
    #endregion
}