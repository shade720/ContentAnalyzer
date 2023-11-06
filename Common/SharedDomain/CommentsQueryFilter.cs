namespace Common.SharedDomain;

public class CommentsQueryFilter
{
    public long Id { get; init; }
    public long PostId { get; init; }
    public long GroupId { get; init; }
    public long AuthorId { get; init; }
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
}