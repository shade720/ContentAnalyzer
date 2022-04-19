namespace Common.EntityFramework;

public class EvaluateResult
{
    public int Id { get; init; }
    public long CommentDataId { get; init; }
    public CommentData CommentData { get; init; }
    public string EvaluateCategory { get; init; }
    public double EvaluateProbability { get; init; }
}