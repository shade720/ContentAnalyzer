using System.Collections.Concurrent;
using VkNet.Model;

namespace VkAPITester;

public class DictionaryStorage : IStorage
{
    private readonly ConcurrentDictionary<int, DataEntry> _data = new();
    
    public void Add(DataEntry entry)
    {
        _data.TryAdd(_data.Count, entry);
    }

    public void AddRange(IEnumerable<DataEntry> entries)
    {
        foreach (var entry in entries) Add(entry);
    }

    public void Clear()
    {
        _data.Clear();
    }

    public static DataEntry Convert(Comment comment)
    {
        return new DataEntry(
            comment.Id,
            comment.PostId ?? 0,
            comment.OwnerId ?? 0,
            comment.FromId ?? 0,
            comment.Text,
            comment.Date ?? new DateTime(0, 0, 0));
    }
    public static IEnumerable<DataEntry> ConvertAll(IEnumerable<Comment>  comments)
    {
        return comments.Select(Convert).ToList();
    }
}