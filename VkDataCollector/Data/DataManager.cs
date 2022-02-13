using VkNet.Model;

namespace VkDataCollector.Data;

internal class DataManager
{
    public delegate void NewData(DataFrame entry);
    public event NewData? OnNewDataArrivedEvent;

    public void SendData(DataFrame entry)
    {
        OnNewDataArrivedEvent?.Invoke(entry);
    }

    public void SendAllData(IEnumerable<DataFrame> entries)
    {
        foreach (var entry in entries)
        {
            OnNewDataArrivedEvent?.Invoke(entry);
        }
    }

    public static DataFrame Convert(Comment comment)
    {
        return new DataFrame(
            comment.Id,
            comment.PostId ?? 0,
            comment.OwnerId ?? 0,
            comment.FromId ?? 0,
            comment.Text,
            comment.Date ?? new DateTime(0, 0, 0));
    }
    public static IEnumerable<DataFrame> ConvertAll(IEnumerable<Comment>  comments)
    {
        return comments.Select(Convert).ToList();
    }
}

