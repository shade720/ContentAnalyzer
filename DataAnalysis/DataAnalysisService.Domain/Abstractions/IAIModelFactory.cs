namespace DataAnalysisService.Domain.Abstractions;

public interface IAIModelFactory
{
    public IAIModel CreateAIModel(string configurationKey);
}