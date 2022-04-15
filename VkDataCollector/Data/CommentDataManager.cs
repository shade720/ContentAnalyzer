using System.Text;
using System.Text.RegularExpressions;
using Common;
using VkNet.Model;

namespace VkDataCollector.Data;

internal class CommentDataManager
{
    public delegate void NewCommentFound(CommentData entry);
    public event NewCommentFound OnNewCommentFoundEvent;

    public void SendData(Comment comment)
    {
        OnNewCommentFoundEvent?.Invoke(Convert(comment));
    }

    public void SendAllData(IEnumerable<Comment> comments)
    {
        foreach (var comment in comments)
        {
            OnNewCommentFoundEvent?.Invoke(Convert(comment));
        }
    }

    private static CommentData Convert(Comment comment)
    {
        return new CommentData
        {
            CommentId = comment.Id,
            PostId = comment.PostId ?? 0,
            GroupId = comment.OwnerId ?? 0,
            AuthorId = comment.FromId ?? 0,
            Text = ClearText(comment.Text),
            PostDate = comment.Date ?? new DateTime(0, 0, 0)
        };
    }

    private static string ClearText(string text)
    {
        var result = UnicodeToUtf8(ClearString(text));
        if (result.StartsWith('[')) result = result.Remove(result.IndexOf('['), result.IndexOf(']') - result.IndexOf('[') + 2).Trim();
        return result;
    }

    private static string UnicodeToUtf8(string text)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var bytes = Encoding.GetEncoding(1251).GetBytes(text);
        return Encoding.GetEncoding(1251).GetString(bytes);
    }

    private static string ClearString(string str)
    {
        var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
        return regex.Replace(str, "");
    }
}

