namespace DataCollectionService.Domain.Abstractions;

public interface ICollector
{
    public delegate Task OnCommentCollected(VkComment comment);
    public event OnCommentCollected OnCommentCollectedEvent;
    public void StartCollecting();
    public void StopCollecting();
}