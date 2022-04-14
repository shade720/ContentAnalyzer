namespace Common;

public class CommentData
{
    public long Id { get; init; }
    public long PostId { get; init; }
    public long GroupId { get; init; }
    public long AuthorId { get; init; }
    public string Text { get; init; }
    public DateTime PostDate { get; init; }
}