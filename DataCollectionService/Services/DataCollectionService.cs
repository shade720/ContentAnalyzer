using System.Diagnostics;
using Common;
using Common.EntityFramework;
using DataCollectionService.DatabaseClients;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataCollectionService.Services;

public class DataCollectionService : DataCollection.DataCollectionBase
{
    private static readonly List<IDataCollector> DataCollectors = new();
    private readonly DatabaseClient<CommentData> _saveDatabase;
    private static readonly Stopwatch _stopwatch = new();

    #region PublicInterface

    public DataCollectionService(IDbContextFactory<CommentsContext> contextFactory)
    {
        _saveDatabase = new AllCommentsDb(contextFactory);
    }

    public override Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        _saveDatabase.Clear();
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StartCollecting();
            dataCollector.Subscribe(InsertToDatabase);
        }
        _stopwatch.Start();
        return Task.FromResult(new StartCollectionServiceReply());
    }

    public override Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StopCollecting();
            dataCollector.Unsubscribe(InsertToDatabase);
        }
        _stopwatch.Stop();
        _stopwatch.Reset();
        return Task.FromResult(new StopCollectionServiceReply());
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
                PostDate = Timestamp.FromDateTime(comment.PostDate.ToUniversalTime()),
                PostId = comment.PostId,
                Text = comment.Text
            });
        }

        return Task.FromResult(new GetCommentsReply {CommentData = { convertedRange } });
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Uptime: {0}", _stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        var logDate = request.LogDate.ToDateTime().ToLocalTime();
        var requiredFilePath = Directory.GetFiles(@"./Logs/", $"log{logDate:yyyyMMdd}*.txt").SingleOrDefault();
        if (string.IsNullOrEmpty(requiredFilePath)) return Task.FromResult(new LogReply());
        if (!File.Exists(requiredFilePath)) return Task.FromResult(new LogReply());
        using var fileStream = new FileStream(requiredFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return Task.FromResult(new LogReply { LogFile = ByteString.FromStream(fileStream) });
    }

    #endregion

    private void InsertToDatabase(CommentData dataFrame)
    {
        _saveDatabase.Add(dataFrame);
    }

    #region Startup

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        DataCollectors.Add(dataCollectorConfiguration.Invoke());
    }
    #endregion
}