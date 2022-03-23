using Interfaces;

namespace DataAnalysisService.AnalyzeModelController;

public interface IAnalyzeModel
{
    public void StartPredictiveListenerScriptAsync();
    public void StartTrainModelScriptAsync();
    public void Predict(ICommentData text);
    public void AbortScript();
}