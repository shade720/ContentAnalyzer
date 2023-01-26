using System.Dynamic;
using Common;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Serilog;
using DataCollectionService.BusinessLogicLayer;

namespace DataCollectionService.Services;

public class DataCollectionAPI : DataCollection.DataCollectionBase
{
    private readonly DataCollector _dataCollector;

    public DataCollectionAPI(DataCollector dataCollector)
    {
        _dataCollector = dataCollector;
    }

    public override Task<StartCollectionServiceReply> StartCollectionService(StartCollectionServiceRequest request, ServerCallContext context)
    {
        _dataCollector.StartCollecting();
        return Task.FromResult(new StartCollectionServiceReply());
    }

    public override Task<StopCollectionServiceReply> StopCollectionService(StopCollectionServiceRequest request, ServerCallContext context)
    {
        _dataCollector.StopCollecting();
        return Task.FromResult(new StopCollectionServiceReply());
    }

    public override Task<GetCommentsReply> GetComments(GetCommentsRequest request, ServerCallContext context)
    {
        var filter = new CommentsQueryFilter
        {
            AuthorId = request.Filter.AuthorId,
            PostId = request.Filter.PostId,
            GroupId = request.Filter.GroupId,
            FromDate = request.Filter.FromDate.ToDateTime(),
            ToDate = request.Filter.ToDate.ToDateTime()
        };
        var range = _dataCollector
            .GetCollectedComments(filter).
            Select(comment => new CommentDataProto
            {
                Id = comment.Id,
                AuthorId = comment.AuthorId,
                CommentId = comment.CommentId,
                GroupId = comment.GroupId,
                PostDate = Timestamp.FromDateTime(comment.PostDate.ToUniversalTime()),
                PostId = comment.PostId,
                Text = comment.Text
            });
        return Task.FromResult(new GetCommentsReply {CommentData = { new RepeatedField<CommentDataProto> { range } } });
    }

    public override Task<ClearCommentsDatabaseReply> ClearCommentsDatabase(ClearCommentsDatabaseRequest request, ServerCallContext context)
    {
        _dataCollector.ClearCollectedComments();
        return Task.FromResult(new ClearCommentsDatabaseReply());
    }

    public override Task<LogReply> GetLogs(LogRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Uptime: {0}", _dataCollector.CurrentWorkingTime);

        var logDate = request.LogDate.ToDateTime().ToLocalTime();

        var requiredFilePath = Directory.GetFiles(@"./Logs/", $"log{logDate:yyyyMMdd}*.txt").SingleOrDefault();

        if (requiredFilePath is null)
        {
            Log.Logger.Error("Log file for {date} does not exist", logDate.ToString("yyyyMMdd"));
            return Task.FromResult(new LogReply());
        }

        using var fileStream = new FileStream(requiredFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        return Task.FromResult(new LogReply { LogFile = ByteString.FromStream(fileStream) });
    }

    public override Task<SetConfigurationReply> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        Log.Logger.Information("Updating settings...");

        var success = _dataCollector.UpdateSettings(request.Settings);
        if (!success) 
            return Task.FromResult(new SetConfigurationReply());

        Log.Logger.Information("Settings file updated");

        if (!_dataCollector.IsRunning) return Task.FromResult(new SetConfigurationReply());
        Log.Logger.Information("Restarting service...");
        
        _dataCollector.RestartCollecting();

        Log.Logger.Information("Service restarted on new configuration");
        return Task.FromResult(new SetConfigurationReply());
    }
}