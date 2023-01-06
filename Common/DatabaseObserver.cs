using Common.EntityFramework;

namespace Common;

public abstract class DatabaseObserver
{
    public delegate void OnData(Comment data);
    public OnData OnDataEvent;

    public bool IsLoadingStarted { get; protected set; }

    public abstract void StartLoading();

    public abstract void StopLoading();
}