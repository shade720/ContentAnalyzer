using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAnalysisService.DatabaseClients;

public class AllCommentsDb : DatabaseObserver
{
    private long _lastReceivedId;
    private CancellationTokenSource _cancellation;
    private Action<CommentData> _dataProcessor;
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    private readonly int _observeDelay;

    public AllCommentsDb(IDbContextFactory<CommentsContext> contextFactory, int observeDelayMs)
    {
        _contextFactory = contextFactory;
        _observeDelay = observeDelayMs;
    }

    #region PublicInterface

    public override void StartLoading()
    {
        if (IsLoadingStarted) throw new Exception("Loading already started");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");
        //Connect();
        _cancellation = new CancellationTokenSource();
        IsLoadingStarted = true;
        var result = LoadingLoop(_cancellation.Token);
        Log.Logger.Information("Loading started on with delay {_observeDelay}", _observeDelay);
    }

    public override void StopLoading()
    {
        if (!IsLoadingStarted) throw new Exception($"Loading already stopped {nameof(StopLoading)}");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");

        _cancellation.Cancel();
        IsLoadingStarted = false;
        //Disconnect();
        Log.Logger.Information("Loading stopped");
    }

    public override void OnDataArrived(Action<CommentData> handler) => _dataProcessor = handler;

    #endregion

    private async Task LoadingLoop(CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                LoadData();
                await Task.Delay(_observeDelay, cancellationToken);
                Log.Logger.Information("Loading loop working");
            }
        }, cancellationToken);
        Log.Logger.Information("Loading loop break");
    }

    private void LoadData()
    {
        using var context = _contextFactory.CreateDbContext();
        var newComments = context.Comments.Where(c => c.Id > _lastReceivedId && c.EvaluateResults.Count == 0);
        foreach (var comment in newComments)
        {
            _lastReceivedId = comment.Id;
            _dataProcessor?.Invoke(comment);
        }
    }
}