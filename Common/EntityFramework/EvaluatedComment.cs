namespace Common.EntityFramework;

public class EvaluatedComment
{
    public int Id { get; init; }
    public long CommentId { get; init; }
    public Comment RelatedComment { get; init; }
    public string EvaluateCategory { get; init; }
    public double EvaluateProbability { get; init; }
}