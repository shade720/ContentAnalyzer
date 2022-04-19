
using Common.EntityFramework;

namespace Common;

public abstract class DatabaseObserver
{
    public bool IsLoadingStarted { get; protected set; }

    public abstract void StartLoading();

    public abstract void StopLoading();

    public abstract void OnDataArrived(Action<CommentData> handler);
}