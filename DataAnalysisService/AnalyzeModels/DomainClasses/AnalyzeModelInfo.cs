namespace DataAnalysisService.AnalyzeModels.DomainClasses;

public class AnalyzeModelInfo
{
    public string Interpreter { get; set; }
    public string PredictScript { get; set; }
    public string TrainScript { get; set; }
    public string DataSet { get; set; }
    public string Model { get; set; }
    public string[] Categories { get; set; }
    public int EvaluateThresholdPercent { get; set; }
}