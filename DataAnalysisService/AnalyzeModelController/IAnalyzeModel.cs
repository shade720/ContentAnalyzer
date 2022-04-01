using Common;

namespace DataAnalysisService.AnalyzeModelController;

public interface IAnalyzeModel
{
    public void StartPredictiveListener();
    public void StartTrainModel();
    public void Predict(ICommentData text);
    public void StopModel();
}