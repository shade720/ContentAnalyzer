namespace VkAPITester.Models.VkDataCollector;

public class CommentScannersQueue
{
    private readonly Queue<CommentScanner> _queue;
    private readonly int _queueSize;

    public CommentScannersQueue(int queueSize) => (_queueSize, _queue) = (queueSize, new Queue<CommentScanner>(queueSize));

    public bool Contains(long postId)
    {
        return _queue.Any(post => post.PostId == postId);
    }

    public void AddScanner(CommentScanner scanner)
    {
        if (_queue.Count == _queueSize) RemoveScanner(out _);
        scanner.StartScan();
        _queue.Enqueue(scanner);
        Console.WriteLine("Comment scanner added to queue");
    }

    private void RemoveScanner(out CommentScanner result)
    {
        result = _queue.Dequeue();
        result.StopScan();
        Console.WriteLine($"Delete and unsubscribe comment scanner {result.PostId}");
    }
    public void Clear()
    {
        for (var i = 0; i < _queue.Count; i++)
        {
            RemoveScanner(out _);
        }
    }
}