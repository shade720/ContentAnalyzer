using Interfaces;

namespace DataAnalysisService.Databases.SqlServer;

public interface IDatabaseObserver
{
    public bool IsLoadingStarted { get; }

    public void StartLoading();

    public void StopLoading();

    public void OnDataArrived(Action<ICommentData> handler);
}