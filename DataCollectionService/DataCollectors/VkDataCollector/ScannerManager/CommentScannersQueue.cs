using DataCollectionService.DataCollectors.VkDataCollector.Scanners;
using Serilog;

namespace DataCollectionService.DataCollectors.VkDataCollector.ScannerManager;

internal class CommentScannersQueue
{
    private readonly FixedQueue<CommentScanner> _queue;

    public CommentScannersQueue(int queueSize)
    {
        _queue = new FixedQueue<CommentScanner>(queueSize);
    }

    public bool Contains(long postId)
    {
        return _queue.Any(post => post.PostId == postId);
    }

    public void AddScanner(CommentScanner scanner)
    {
        scanner.StartScan();
        _queue.Enqueue(scanner);
        Log.Logger.Information("Comment scanner added to queue");
    }

    private void RemoveScanner()
    {
        var result = _queue.Dequeue();
        result.StopScan();
        Log.Logger.Information("Stop and delete comment scanner {0}", result.PostId);
    }

    public void Clear()
    {
        for (var i = 0; i < _queue.Count; i++) RemoveScanner();
        Log.Logger.Information("Queue cleared");
    }
}