using Common;

namespace DataAnalysisService.AnalyzeModelController;

public class Category : ICategory
{
    public string Title { get; init; }
    public double PredictValue { get; init; }
}