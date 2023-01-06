using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAnalysisService.BusinessLogicLayer.DatabaseClients;

public class CommentsDatabaseObserver : DatabaseObserver
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    private readonly IConfiguration _configuration;
    private CancellationTokenSource _stopLoadingToken;

    public CommentsDatabaseObserver(IDbContextFactory<CommentsContext> contextFactory, IConfiguration configuration)
    {
        _contextFactory = contextFactory;
        _configuration = configuration;
    }

    #region PublicInterface

    public override void StartLoading()
    {
        if (IsLoadingStarted) 
            throw new Exception("Loading already started");
        _stopLoadingToken = new CancellationTokenSource();
        var _ = StartLoadingLoop();
        IsLoadingStarted = true;
        Log.Logger.Information("Loading started on with delay {delay}", _configuration["ObserveDelayMs"]);
    }

    public override void StopLoading()
    {
        if (!IsLoadingStarted) 
            throw new Exception($"Loading already stopped {nameof(StopLoading)}");
        _stopLoadingToken.Cancel();
        IsLoadingStarted = false;
        Log.Logger.Information("Loading stopped");
    }

    #endregion

    private async Task StartLoadingLoop()
    {
        while (!_stopLoadingToken.IsCancellationRequested)
        {
            await LoadData();
            await Task.Delay(int.Parse(_configuration["ObserveDelayMs"]), _stopLoadingToken.Token);
            Log.Logger.Information("Loading loop working");
        }
        Log.Logger.Information("Loading loop break");
    }

    private async Task LoadData()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var newComments = context.Comments.Where(c => !context.EvaluatedComments.Any(e => e.CommentId == c.Id));
        foreach (var comment in newComments)
        {
            OnDataEvent(comment);
        }
    }
}