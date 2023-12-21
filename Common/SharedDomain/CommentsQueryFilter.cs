namespace Common.SharedDomain;

public class CommentsQueryFilter
{
    public long Id { get; init; }
    public long PostId { get; init; }
    public long GroupId { get; init; }
    public long AuthorId { get; init; }
    public string Category { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public DateTime FromDate { get; init; } = DateTime.UnixEpoch;
    public DateTime ToDate { get; init; } = DateTime.UnixEpoch;
}