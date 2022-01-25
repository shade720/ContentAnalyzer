namespace VkAPITester;

public class ObservedPostsQueue
{
    private const int QueueSize = 3;
    private readonly Queue<DataLoader> _queue = new (QueueSize);
    private readonly ApiClient _client;
    private readonly AnalyzedDataStorage _storage;

    public ObservedPostsQueue(ApiClient client, AnalyzedDataStorage storage) => (_client, _storage) = (client, storage);

    public bool Contains(long postId)
    {
        return _queue.Any(post => post.PostId == postId);
    }

    public void AddDataSource(long sourceId, long postId)
    {
        if (_queue.Count == QueueSize) RemoveDataSource(out _);

        var dataLoader = new DataLoader(_client, sourceId, postId);

        _storage.AddRange(dataLoader.GetPresentComments());
        var result = dataLoader.WaitOtherComments(_storage);

        _queue.Enqueue(dataLoader);
        Console.WriteLine($"Add to queue post {postId}, group {sourceId}");
    }

    private void RemoveDataSource(out DataLoader result)
    {
        result = _queue.Dequeue();
        result.UnsubscribeToken.Cancel();
    }
    public void Clear()
    {
        for (var i = 0; i < _queue.Count; i++)
        {
            RemoveDataSource(out var result);
            Console.WriteLine($"Delete and unsub post {result.PostId}");
        }
    }
}