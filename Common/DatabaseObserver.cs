using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class DatabaseObserver : Database
{
    protected  DatabaseObserver(DbContextOptions<CommentsContext> options) : base(options) { }

    public bool IsLoadingStarted { get; protected set; }

    public abstract void StartLoading();

    public abstract void StopLoading();

    public abstract void OnDataArrived(Action<CommentData> handler);
}