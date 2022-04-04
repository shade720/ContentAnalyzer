using Common;

namespace DataAnalysisService.AnalyzeModels.DomainClasses;

public class Category : ICategory
{
    public string Title { get; init; }
    public double PredictValue { get; init; }
}