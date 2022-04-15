namespace Common;

public class CommentData
{
    public long Id { get; set; }
    public long CommentId { get; set; }
    public long PostId { get; set; }
    public long GroupId { get; set; }
    public long AuthorId { get; set; }
    public string Text { get; set; }
    public DateTime PostDate { get; set; }
    public List<EvaluateResult> EvaluateResults { get; set; } = new();
}