using Common;

namespace DataAnalysisService.DatabaseClients.SqlServer;

public class CommentData : ICommentData
{
    public CommentData(long id, string text, long postId, long groupId, long authorId, DateTime postDate)
    {
        Id = id;
        Text = text;
        PostId = postId;
        GroupId = groupId;
        AuthorId = authorId;
        PostDate = postDate;
    }
    public long Id { get; }
    public long PostId { get; }
    public long GroupId { get; }
    public long AuthorId { get; }
    public string Text { get; }
    public DateTime PostDate { get; }
}