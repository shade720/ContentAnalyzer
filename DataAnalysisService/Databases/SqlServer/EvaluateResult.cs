using Common;

namespace DataAnalysisService.Databases.SqlServer;

internal class EvaluateResult : IEvaluateResult
{
    public ICommentData CommentData { get; }
    public string EvaluateCategory { get; }
    public double EvaluateProbability { get; }

    public EvaluateResult(ICommentData commentData, string evaluateCategory, double evaluateProbability)
    {
        CommentData = commentData;
        EvaluateCategory = evaluateCategory;
        EvaluateProbability = evaluateProbability;
    }
}