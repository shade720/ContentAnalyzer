using Common.EntityFramework;

namespace DataAnalysisService.Domain.Abstractions;

public interface ICommentsObserver
{
    public delegate Task OnNewInfo(Comment comment);
    public event OnNewInfo? OnNewInfoEvent;
    public void StartObserving();
    public void StopObserving();
}