using System.Globalization;
using Interfaces;

namespace DataAnalysisService.AnalyzeModelController;

public class PredictResult 
{
    public ICommentData CommentData { get; }
    public Category[] Predicts { get; }

    public PredictResult(ICommentData commentData, string predictResult, IReadOnlyList<string> categories)
    {
        CommentData = commentData;
        var predictValues = ParsePredict(predictResult);
        Predicts = new Category[categories.Count];
        if (predictValues.Length == 0)
        {
            Initialize(categories);
            return;
        }
        for (var i = 0; i < categories.Count; i++) Predicts[i] = new Category {Title = categories[i], PredictValue = predictValues[i]};
    }

    private static double[] ParsePredict(string predict)
    {
        var clearResults = predict.Replace("[", "").Replace("]", "");
        var predictValues = clearResults.Split(' ');
        return predictValues.Where(x => x.Length > 1).Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToArray();
    }

    private void Initialize(IReadOnlyList<string> categories)
    {
        for (var i = 0; i < categories.Count; i++) Predicts[i] = new Category { Title = categories[i], PredictValue = 0 };
    }

    public override string ToString()
    {
        return Predicts.Aggregate("", (current, predict) => current + predict.Title + ": " + predict.PredictValue.ToString("F5") + " ");
    }
}