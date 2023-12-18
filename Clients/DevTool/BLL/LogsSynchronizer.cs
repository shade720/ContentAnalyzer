namespace DevTool.BLL;

public class LogsSynchronizer
{
    private readonly string _localLogFolder;
    private readonly CollectionServiceClient _collectionServiceClient;
    private readonly AnalysisServiceClient _analysisServiceClient;

    private CancellationTokenSource? _cancellationTokenSource;

    public LogsSynchronizer(
        string localLogFolder,
        CollectionServiceClient collectionServiceClient, 
        AnalysisServiceClient analysisServiceClient)
    {
        _localLogFolder = localLogFolder;
        _collectionServiceClient = collectionServiceClient;
        _analysisServiceClient = analysisServiceClient;
    }

    public void StartSynchronizing()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                Poll();
                Thread.Sleep(1000);
            }
        }, _cancellationTokenSource.Token);
    }

    public void StopSynchronizing()
    {
        _cancellationTokenSource?.Cancel();
    }

    private void Poll()
    {
        //_collectionServiceClient.GetLogFile();
    }
}