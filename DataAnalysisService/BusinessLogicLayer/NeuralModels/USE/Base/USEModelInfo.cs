namespace DataAnalysisService.BusinessLogicLayer.NeuralModels.USE.Base;

public class USEModelInfo
{
    public string Interpreter { get; init; }
    public string PredictScript { get; init; }
    public string TrainScript { get; set; }
    public string DataSet { get; set; }
    public string Model { get; init; }
    public string[] Categories { get; init; }
}