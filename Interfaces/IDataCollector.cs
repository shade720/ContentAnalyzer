namespace Common;

public interface IDataCollector
{
    public void AddCommunity(long communityId);
    public void StartCollecting();
    public void StopCollecting();
    public void Subscribe(Action<ICommentData> handler);
}