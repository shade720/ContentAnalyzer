using Common;
using System.Runtime.ExceptionServices;
using Serilog;
using VkDataCollector.Scanners;

namespace VkDataCollector.ScannerManager;

internal class CommentScannersQueue
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
        try
        {
            if (_queue.Count == _queueSize) RemoveScanner(out _);
            scanner.StartScan();
            _queue.Enqueue(scanner);
            Log.Logger.Information("Comment scanner added to queue");
        }
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            throw;
        }
    }

    private void RemoveScanner(out CommentScanner result)
    {
        if (_queue.Count == 0)
        {
            Log.Logger.Error("Comments scanners queue already cleared");
            result = null;
            return;
        }
        result = _queue.Dequeue();
        result.StopScan();
        Log.Logger.Information("Stop and delete comment scanner {result.PostId}", result.PostId);
    }
    public void Clear()
    {
        for (var i = 0; i < _queue.Count; i++)
        {
            RemoveScanner(out _);
        }
        Log.Logger.Information("Queue cleared");
    }
}