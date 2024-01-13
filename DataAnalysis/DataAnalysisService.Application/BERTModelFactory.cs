using DataAnalysisService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace DataAnalysisService.Application;

public class BertModelFactory : IAIModelFactory
{
    private readonly IConfiguration _configuration;

    public BertModelFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IAIModel CreateAIModel(string configurationKey)
    {
        var modelConfig = _configuration.GetSection("BertModels").GetSection(configurationKey);
        return new BertModel(
            configurationKey,
            modelConfig["Vocabulary"],
            modelConfig["ONNXBertModel"],
            modelConfig["LabelEncoding"]);
    }
}