using Xunit;

namespace DataAnalysisService.Application.Tests;

public class BertModelFactoryTests
{
    [Fact]
    public void CreateArtificialIntelligenceModel_Test()
    {
        // Arrange
        const string modelConfigurationKey = "SensetiveTopicsBERTModel";
        var config = ConfigurationProvider.GetConfiguration();
        var sut = new BertModelFactory(config);

        // Act
        var actualModel = sut.CreateArtificialIntelligenceModel(modelConfigurationKey);
        
        // Assert
        Assert.Equal(modelConfigurationKey, actualModel.Title);

        // Teardown
        actualModel.Dispose();
    }
}