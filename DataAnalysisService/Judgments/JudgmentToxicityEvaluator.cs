using System.Globalization;
using M_USE_Toxic;

namespace DataAnalysisService.Judgments;

internal class JudgmentToxicityEvaluator
{
    private readonly int _toxicityThreshold;
    private readonly int _expressionLength;

    public JudgmentToxicityEvaluator(int toxicityThreshold = 90, int expressionLength = 60)
    {
        _toxicityThreshold = toxicityThreshold;
        _expressionLength = expressionLength;
    }

    public bool IsToxic(PredictResult expression)
    {
       return double.Parse(expression.Toxicity, CultureInfo.InvariantCulture) > _toxicityThreshold && expression.DataFrame.Text.Length > _expressionLength;
    }
}