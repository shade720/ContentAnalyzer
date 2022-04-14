using Common;

namespace DataAnalysisService.DatabaseClients;

public class AllCommentsDb : DatabaseObserver
{
    private long _lastReceivedId;
    private CancellationTokenSource _cancellation;
    private Action<CommentData> _dataProcessor;
    private readonly int _observeDelay;

    public AllCommentsDb(string connectionString, int observeDelayMs) : base(connectionString) => 
        _observeDelay = observeDelayMs;
    

    #region PublicInterface

    public override void StartLoading()
    {
        if (IsLoadingStarted) throw new Exception("Loading already started");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");

        Connect();
        _cancellation = new CancellationTokenSource();
        IsLoadingStarted = true;
        var result = LoadingLoop(_cancellation.Token);
        Logger.Log($"Loading started on with delay {_observeDelay}", Logger.LogLevel.Information);
    }

    public override void StopLoading()
    {
        if(!IsLoadingStarted) throw new Exception($"Loading already stopped {nameof(StopLoading)}");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");

        _cancellation.Cancel();
        IsLoadingStarted = false;
        Disconnect();
        Logger.Log("Loading stopped", Logger.LogLevel.Information);
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
            }
        }, cancellationToken);
    }

    private void LoadData()
    {
        var newComments = from c in Context.Comments where c.Id > _lastReceivedId select c;
        foreach (var comment in newComments)
        {
            _lastReceivedId = comment.Id;
            _dataProcessor?.Invoke(comment);
        }
    }
}