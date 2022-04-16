using Common;
using System.Runtime.ExceptionServices;
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
            Logger.Log("Comment scanner added to queue", Logger.LogLevel.Information);
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
            Logger.Log("Comments scanners queue already cleared", Logger.LogLevel.Error);
            result = null;
            return;
        }
        result = _queue.Dequeue();
        result.StopScan();
        Logger.Log($"Stop and delete comment scanner {result.PostId}", Logger.LogLevel.Information);
    }
    public void Clear()
    {
        for (var i = 0; i < _queue.Count; i++)
        {
            RemoveScanner(out _);
        }
        Logger.Log("Queue cleared", Logger.LogLevel.Information);
    }
}