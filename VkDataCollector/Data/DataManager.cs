using System.Text;
using System.Text.RegularExpressions;
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
            ClearText(comment.Text),
            comment.Date ?? new DateTime(0, 0, 0));
    }
    public static IEnumerable<DataFrame> ConvertAll(IEnumerable<Comment>  comments)
    {
        return comments.Select(Convert).ToList();
    }

    private static string ClearText(string text)
    {
        var result = UnicodeToUtf8(ProcessString(text));
        if (result.Contains('[')) result = result.Remove(result.IndexOf('['), result.IndexOf(']') - result.IndexOf('[') + 2).Trim();
        //result = result.Replace("?", "");
        return result;
    }

    private static string UnicodeToUtf8(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return Encoding.UTF8.GetString(bytes);
    }

    private static string ProcessString(string str)
    {
        var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
        return regex.Replace(str, "");
    }
}

