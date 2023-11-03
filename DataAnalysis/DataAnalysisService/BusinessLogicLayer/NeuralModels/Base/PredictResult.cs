namespace DataAnalysisService.BusinessLogicLayer.NeuralModels.Base;

public class PredictResult
{
    public string Category { get; init; }
    public double Probability { get; init; }
}