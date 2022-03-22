using Interfaces;

namespace DataAnalysisService.Databases.SqlServer;

public interface IDatabaseObserver
{
    public void StartLoading();

    public void StopLoading();

    public void OnDataArrived(Action<IDataFrame> handler);
}