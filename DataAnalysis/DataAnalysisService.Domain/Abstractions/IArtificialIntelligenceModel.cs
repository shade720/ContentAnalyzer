namespace DataAnalysisService.Domain.Abstractions;

public interface IArtificialIntelligenceModel : IDisposable
{
    public string Title { get; }
    public PredictResult Predict(string sentence);
    public void Dispose();
}