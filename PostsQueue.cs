namespace VkAPITester;

public class PostsQueue
{
    private readonly Queue<DataCollector> _queue;
    private readonly int _queueSize;

    public PostsQueue(int queueSize) => (_queueSize, _queue) = (queueSize, new Queue<DataCollector>(queueSize));

    public bool Contains(long postId)
    {
        return _queue.Any(post => post.PostId == postId);
    }

    public void AddDataSource(DataCollector collector)
    {
        if (_queue.Count == _queueSize) RemoveDataSource(out _);
        _queue.Enqueue(collector);
    }

    private void RemoveDataSource(out DataCollector result)
    {
        result = _queue.Dequeue();
        result.UnsubscribeToken.Cancel();
        Console.WriteLine($"Delete and unsubscribe post {result.PostId}");
    }
    public void Clear()
    {
        for (var i = 0; i < _queue.Count; i++)
        {
            RemoveDataSource(out _);
        }
    }
}