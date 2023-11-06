using Common.EntityFramework;
using DataCollectionService.Domain;

namespace DataCollectionService.Grpc.UI;

public static class CommentsConverter
{
    public static VkComment ConvertToVkComment(Comment comment)
    {
        return new VkComment
        {
            CommentId = comment.CommentId,
            PostId = comment.PostId,
            AuthorId = comment.AuthorId,
            GroupId = comment.GroupId,
            PostDate = comment.PostDate,
            Text = comment.Text
        };
    }

    public static Comment ConvertFromVkComment(VkComment comment)
    {
        return new Comment
        {
            CommentId = comment.CommentId,
            PostId = comment.PostId,
            AuthorId = comment.AuthorId,
            GroupId = comment.GroupId,
            PostDate = comment.PostDate,
            Text = comment.Text
        };
    }
}