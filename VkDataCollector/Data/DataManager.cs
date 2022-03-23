using System.Text;
using System.Text.RegularExpressions;
using VkNet.Model;

namespace VkDataCollector.Data;

internal class DataManager
{
    public delegate void NewData(CommentData entry);
    public event NewData? OnNewDataArrivedEvent;

    public void SendData(CommentData entry)
    {
        OnNewDataArrivedEvent?.Invoke(entry);
    }

    public void SendAllData(IEnumerable<CommentData> entries)
    {
        foreach (var entry in entries)
        {
            OnNewDataArrivedEvent?.Invoke(entry);
        }
    }

    public static CommentData Convert(Comment comment)
    {
        return new CommentData(
            comment.Id,
            comment.PostId ?? 0,
            comment.OwnerId ?? 0,
            comment.FromId ?? 0,
            ClearText(comment.Text),
            comment.Date ?? new DateTime(0, 0, 0));
    }
    public static IEnumerable<CommentData> ConvertAll(IEnumerable<Comment>  comments)
    {
        return comments.Select(Convert).ToList();
    }

    private static string ClearText(string text)
    {
        var result = UnicodeToUtf8(ProcessString(text));
        if (result.StartsWith('[')) result = result.Remove(result.IndexOf('['), result.IndexOf(']') - result.IndexOf('[') + 2).Trim();
        //result = result.Replace("?", "");
        return result;
    }

    private static string UnicodeToUtf8(string text)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var bytes = Encoding.GetEncoding(1251).GetBytes(text);
        return Encoding.GetEncoding(1251).GetString(bytes);
    }

    private static string ProcessString(string str)
    {
        var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
        return regex.Replace(str, "");
    }
}

