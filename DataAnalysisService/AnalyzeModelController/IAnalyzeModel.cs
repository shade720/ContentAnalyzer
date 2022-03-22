using Interfaces;

namespace DataAnalysisService.AnalyzeModelController;

public interface IAnalyzeModel
{
    public Task StartPredictiveListenerScriptAsync();
    public Task StartTrainModelScriptAsync();
    public void Predict(IDataFrame text);
    public void AbortScript();
}