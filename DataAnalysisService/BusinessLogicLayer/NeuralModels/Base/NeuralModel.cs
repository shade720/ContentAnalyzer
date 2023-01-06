namespace DataAnalysisService.BusinessLogicLayer.NeuralModels.Base;

public abstract class NeuralModel : IDisposable
{
    public string Title { get; protected init; }

    public bool IsInitialized { get; protected set; }

    public abstract void Initialize();

    public abstract PredictResult Predict(string sentence);

    public abstract void Dispose();
}