using DataAnalysisService.Domain;
using Xunit;

namespace DataAnalysisService.Application.Tests;

public class BertModelTests : IClassFixture<BertModelFixture>
{
    private readonly BertModelFixture _bertModelFixture;

    public BertModelTests(BertModelFixture bertModelFixture)
    {
        _bertModelFixture = bertModelFixture;
    }

    [Fact]
    public void Predict_Test_Terrorism()
    {
        // Arrange
        const string expressionForPredict = "я террорист";
        var expectedPredictResult = new PredictResult
        {
            Category = "terrorism",
            Probability = 0.9999
        };

        // Act
        var actualPredictResult = _bertModelFixture.Sut.Predict(expressionForPredict);

        // Assert
        Assert.Equal(actualPredictResult.Category, actualPredictResult.Category);
        Assert.Equal(expectedPredictResult.Probability, actualPredictResult.Probability, 3);
    }

    [Fact]
    public void Predict_Test_Politics()
    {
        // Arrange
        const string expressionForPredict = "Как только над Путиным не изгаляются...";
        var expectedPredictResult = new PredictResult
        {
            Category = "politics",
            Probability = 0.9999
        };

        // Act
        var actualPredictResult = _bertModelFixture.Sut.Predict(expressionForPredict);

        // Assert
        Assert.Equal(actualPredictResult.Category, actualPredictResult.Category);
        Assert.Equal(expectedPredictResult.Probability, actualPredictResult.Probability, 4);
    }
}

public class BertModelFixture
{
    public readonly BertModel Sut;

    public BertModelFixture()
    {
        const string key = "SensetiveTopicsBERTModel";
        var config = ConfigurationProvider
            .GetConfiguration()
            .GetSection("BertModels")
            .GetSection(key);

        Sut = new BertModel(
            key,
            config["Vocabulary"],
            config["ONNXBertModel"],
            config["LabelEncoding"]);
    }
}