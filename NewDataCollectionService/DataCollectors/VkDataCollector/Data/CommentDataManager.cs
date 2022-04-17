using System.Text.RegularExpressions;
using Common.EntityFramework;
using VkNet.Model;

namespace DataCollectionService.DataCollectors.VkDataCollector.Data;

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
            if (IsCommentInvalid(comment)) continue;
            OnNewCommentFoundEvent?.Invoke(Convert(comment));
        }
    }

    private static CommentData Convert(Comment comment)
    {
        return new CommentData
        {
            CommentId = comment.Id,
            PostId = comment.PostId.Value,
            GroupId = comment.OwnerId.Value,
            AuthorId = comment.FromId.Value,
            Text = ClearText(comment.Text),
            PostDate = comment.Date.Value
        };
    }

    private static string ClearText(string text)
    {
        var result = ClearString(text);
        if (result.StartsWith('[')) result = result.Remove(result.IndexOf('['), result.IndexOf(']') - result.IndexOf('[') + 2).Trim();
        return result;
    }

    private static bool IsCommentInvalid(Comment comment)
    {
        return comment.Id <= 0 || comment.PostId <= 0 || comment.OwnerId > 0 || string.IsNullOrEmpty(comment.Text) || comment.Text.Length <= 5;
    }

    private static string ClearString(string str)
    {
        var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
        return regex.Replace(str, "");
    }
}

