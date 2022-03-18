namespace Interfaces;

public interface IAnalyzeModel
{
    public Task StartPredictiveListenerScriptAsync(string predictScript, string model);
    public Task StartTrainModelScriptAsync(string trainScript, string dataSet);
    public void Predict(IDataFrame text);
    public void AbortScript();
}