using System.Collections;

namespace DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient;

public class FixedQueue<T> : IEnumerable<T>
{
    private readonly int _limit;
    private readonly Queue<T> _queue;
    public int Count => _queue.Count;

    public FixedQueue(int limit)
    {
        _limit = limit;
        _queue = new Queue<T>(_limit);
    }
    public void Enqueue(T item)
    {
        if (item is null) return;
        if (_queue.Count == _limit) _queue.Dequeue();
        _queue.Enqueue(item);
    }
    public T Dequeue()
    {
        return _queue.Dequeue();
    }

    public void Clear()
    {
        _queue.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _queue.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_queue).GetEnumerator();
    }
}