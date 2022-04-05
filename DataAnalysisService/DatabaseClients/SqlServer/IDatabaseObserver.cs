using Common;

namespace DataAnalysisService.DatabaseClients.SqlServer;

public interface IDatabaseObserver
{
    public bool IsLoadingStarted { get; }

    public void StartLoading();

    public void StopLoading();

    public void OnDataArrived(Action<ICommentData> handler);
}