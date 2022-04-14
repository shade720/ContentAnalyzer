namespace Common;

public class EvaluateResult
{
    public int Id { get; set; }
    public CommentData CommentData { get; set; }
    public string EvaluateCategory { get; set; }
    public double EvaluateProbability { get; set; }

    public override string ToString()
    {
        return $"{CommentData.Text} {EvaluateCategory} {EvaluateProbability}";
    }
}