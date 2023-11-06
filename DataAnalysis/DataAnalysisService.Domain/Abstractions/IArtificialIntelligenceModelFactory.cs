namespace DataAnalysisService.Domain.Abstractions;

public interface IArtificialIntelligenceModelFactory
{
    public IArtificialIntelligenceModel CreateArtificialIntelligenceModel(string configurationKey);
}