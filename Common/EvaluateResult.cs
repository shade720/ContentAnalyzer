namespace Common;

public class EvaluateResult
{
    public int Id { get; set; }
    public long CommentDataId { get; set; }
    public CommentData CommentData { get; set; }
    public string EvaluateCategory { get; set; }
    public double EvaluateProbability { get; set; }
}