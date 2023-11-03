namespace DataCollectionService.Domain;

public class CommentsQueryFilter
{
    public long Id { get; set; }
    public long PostId { get; set; }
    public long GroupId { get; set; }
    public long AuthorId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}