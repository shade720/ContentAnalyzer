namespace Common;

public interface IEvaluateResult
{
    public ICommentData CommentData { get; }
    public string EvaluateCategory { get; }
    public double EvaluateProbability { get; }
}