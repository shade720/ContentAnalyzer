using System.Text.RegularExpressions;
using Common.EntityFramework;
using VkNet.Model;
using Comment = Common.EntityFramework.Comment;

namespace DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Data;

internal class CommentDataManager
{
    public delegate void NewCommentFound(Comment entry);
    public event NewCommentFound? OnNewCommentFoundEvent;

    public void SendCommentData(VkNet.Model.Comment comment)
    {
        if (IsCommentInvalid(comment)) return;
        var convertedComment = Convert(comment);
        if (string.IsNullOrEmpty(convertedComment.Text) || convertedComment.Text.Length <= 5) return;
        OnNewCommentFoundEvent?.Invoke(convertedComment);
    }

    public void SendAllCommentData(IEnumerable<VkNet.Model.Comment> comments)
    {
        foreach (var comment in comments) SendCommentData(comment);
    }

    private static Comment Convert(VkNet.Model.Comment comment)
    {
        return new Comment
        {
            CommentId = comment.Id,
            PostId = comment.PostId!.Value,
            GroupId = comment.OwnerId!.Value,
            AuthorId = comment.FromId!.Value,
            Text = ClearText(comment.Text),
            PostDate = comment.Date!.Value.ToLocalTime()
        };
    }

    private static string ClearText(string text)
    {
        var result = Regex.Replace(text, @"\p{Cs}", "");
        if (result.StartsWith('[')) result = result.Remove(result.IndexOf('['), result.IndexOf(']') - result.IndexOf('[') + 2).Trim();
        return result;
    }

    private static bool IsCommentInvalid(VkNet.Model.Comment comment)
    {
        return comment.PostId is null ||
               comment.OwnerId is null ||
               comment.FromId is null ||
               comment.Date is null ||
               comment.Id <= 0 ||
               comment.PostId <= 0 ||
               comment.OwnerId > 0 ||
               string.IsNullOrEmpty(comment.Text) ||
               comment.Text.Length <= 5;
    }
}

