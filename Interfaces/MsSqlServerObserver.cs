namespace Common;

public abstract class MsSqlServerObserver : MsSqlServer
{
    protected  MsSqlServerObserver(string connectionString) : base(connectionString)
    {
    }
    public bool IsLoadingStarted { get; protected set; }

    public abstract void StartLoading();

    public abstract void StopLoading();

    public abstract void OnDataArrived(Action<ICommentData> handler);
}