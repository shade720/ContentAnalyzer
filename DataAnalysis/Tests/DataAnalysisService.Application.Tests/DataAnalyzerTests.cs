using Common.SharedDomain;
using DataAnalysisService.Domain;
using DataAnalysisService.Domain.Abstractions;
using Moq;
using Xunit;

namespace DataAnalysisService.Application.Tests;

public class DataAnalyzerTests
{
    private readonly DataAnalyzer _sut;

    private readonly Mock<IArtificialIntelligenceModelFactory> _artificialIntelligenceModelFactoryMock;
    private readonly Mock<ICommentsObserver> _commentsObserverMock;
    private readonly Mock<IEvaluatedCommentsRepository> _evaluatedCommentsRepositoryMock;
    private readonly Mock<IArtificialIntelligenceModel> _artificialIntelligenceModelMock;

    public DataAnalyzerTests()
    {
        _artificialIntelligenceModelFactoryMock = new Mock<IArtificialIntelligenceModelFactory>();
        _commentsObserverMock = new Mock<ICommentsObserver>();
        _evaluatedCommentsRepositoryMock = new Mock<IEvaluatedCommentsRepository>();
        _artificialIntelligenceModelMock = new Mock<IArtificialIntelligenceModel>();

        var config = ConfigurationProvider.GetConfiguration();
        _sut = new DataAnalyzer(
            config,
            _artificialIntelligenceModelFactoryMock.Object,
            _commentsObserverMock.Object,
            _evaluatedCommentsRepositoryMock.Object);
    }

    [Fact]
    public async Task DataAnalyzer_Success_Once()
    {
        // Arrange
        const string modelKey = "SensetiveTopicsBERTModel";
        const bool expectedIsStarted = true;
        const string text = "я террорист";
        var expectedComment = new Comment
        {
            Id = 450,
            CommentId = 1,
            GroupId = 2,
            PostId = 2,
            AuthorId = 3,
            PostDate = new DateTime(2023, 11, 06),
            Text = text
        };
        var expectedPredictResult = new PredictResult
        {
             Category = "terrorism",
             Probability = 0.9999
        };
        var evaluatedComment = new EvaluatedComment
        {
            CommentId = expectedComment.Id,
            RelatedComment = expectedComment,
            EvaluateCategory = "terrorism",
            EvaluateProbability = 0.9999
        };

        _artificialIntelligenceModelMock
            .Setup(x => x.Title)
            .Returns(modelKey);

        _artificialIntelligenceModelMock
            .Setup(x => x.Predict(text))
            .Returns(expectedPredictResult);

        _artificialIntelligenceModelFactoryMock
            .Setup(x => x.CreateArtificialIntelligenceModel(modelKey))
            .Returns(_artificialIntelligenceModelMock.Object);

        // Act

        _sut.StartAnalysis();

        var actualIsStarted = _sut.IsAnalysisStarted;

        await _commentsObserverMock.RaiseAsync(a => a.OnNewInfoEvent += null, expectedComment);

        _sut.StopAnalysis();

        // Assert
        Assert.Equal(expectedIsStarted, actualIsStarted);

        _artificialIntelligenceModelFactoryMock
            .Verify(x => x.CreateArtificialIntelligenceModel(modelKey),  Times.Once());

        _commentsObserverMock
            .Verify(x => x.StartObserving(), Times.Once);

        _commentsObserverMock
            .Verify(x => x.StopObserving(), Times.Once);

        _evaluatedCommentsRepositoryMock
            .Verify(x => x.Add(It.Is<EvaluatedComment>(x => 
                x.CommentId == evaluatedComment.CommentId &&
                x.EvaluateCategory == evaluatedComment.EvaluateCategory &&
                Math.Abs(x.EvaluateProbability - evaluatedComment.EvaluateProbability) < 0.01)), Times.Once);

        _artificialIntelligenceModelMock
            .Verify(x => x.Predict(text), Times.Once);

        _artificialIntelligenceModelMock
            .Verify(x => x.Dispose(), Times.Once);
    }

    [Fact]
    public async Task DataAnalyzer_Success_Twice()
    {
        // Arrange
        const string modelKey = "SensetiveTopicsBERTModel";
        const bool expectedIsStarted = true;
        const string text = "я террорист";
        var expectedComment = new Comment
        {
            Id = 450,
            CommentId = 1,
            GroupId = 2,
            PostId = 2,
            AuthorId = 3,
            PostDate = new DateTime(2023, 11, 06),
            Text = text
        };
        var expectedPredictResult = new PredictResult
        {
            Category = "terrorism",
            Probability = 0.9999
        };
        var evaluatedComment = new EvaluatedComment
        {
            CommentId = expectedComment.Id,
            RelatedComment = expectedComment,
            EvaluateCategory = "terrorism",
            EvaluateProbability = 0.9999
        };

        _artificialIntelligenceModelMock
            .Setup(x => x.Title)
            .Returns(modelKey);

        _artificialIntelligenceModelMock
            .Setup(x => x.Predict(text))
            .Returns(expectedPredictResult);

        _artificialIntelligenceModelFactoryMock
            .Setup(x => x.CreateArtificialIntelligenceModel(modelKey))
            .Returns(_artificialIntelligenceModelMock.Object);

        // Act

        _sut.StartAnalysis();

        var actualIsStarted = _sut.IsAnalysisStarted;

        await _commentsObserverMock.RaiseAsync(a => a.OnNewInfoEvent += null, expectedComment);

        await Task.Delay(1000);

        await _commentsObserverMock.RaiseAsync(a => a.OnNewInfoEvent += null, expectedComment);

        _sut.StopAnalysis();

        // Assert
        Assert.Equal(expectedIsStarted, actualIsStarted);

        _artificialIntelligenceModelFactoryMock
            .Verify(x => x.CreateArtificialIntelligenceModel(modelKey), Times.Once());

        _commentsObserverMock
            .Verify(x => x.StartObserving(), Times.Once);

        _commentsObserverMock
            .Verify(x => x.StopObserving(), Times.Once);

        _evaluatedCommentsRepositoryMock
            .Verify(x => x.Add(It.Is<EvaluatedComment>(x =>
                x.CommentId == evaluatedComment.CommentId &&
                x.EvaluateCategory == evaluatedComment.EvaluateCategory &&
                Math.Abs(x.EvaluateProbability - evaluatedComment.EvaluateProbability) < 0.01)), Times.Exactly(2));

        _artificialIntelligenceModelMock
            .Verify(x => x.Predict(text), Times.Exactly(2));

        _artificialIntelligenceModelMock
            .Verify(x => x.Dispose(), Times.Once);
    }

    [Fact]
    public async Task DataAnalyzer_Bad_Sentence()
    {
        // Arrange
        const string modelKey = "SensetiveTopicsBERTModel";
        const bool expectedIsStarted = true;
        const string badText = " ";
        const string text = "я террорист";
        var badComment = new Comment
        {
            Id = 450,
            CommentId = 1,
            GroupId = 2,
            PostId = 2,
            AuthorId = 3,
            PostDate = new DateTime(2023, 11, 06),
            Text = badText
        };
        var validComment = new Comment
        {
            Id = 450,
            CommentId = 1,
            GroupId = 2,
            PostId = 2,
            AuthorId = 3,
            PostDate = new DateTime(2023, 11, 06),
            Text = text
        };
        var expectedPredictResult = new PredictResult
        {
            Category = "terrorism",
            Probability = 0.9999
        };
        var evaluatedComment = new EvaluatedComment
        {
            CommentId = badComment.Id,
            RelatedComment = badComment,
            EvaluateCategory = "terrorism",
            EvaluateProbability = 0.9999
        };

        _artificialIntelligenceModelMock
            .Setup(x => x.Title)
            .Returns(modelKey);

        _artificialIntelligenceModelMock
            .Setup(x => x.Predict(badText))
            .Throws<Exception>();

        _artificialIntelligenceModelMock
            .Setup(x => x.Predict(text))
            .Returns(expectedPredictResult);

        _artificialIntelligenceModelFactoryMock
            .Setup(x => x.CreateArtificialIntelligenceModel(modelKey))
            .Returns(_artificialIntelligenceModelMock.Object);

        // Act

        _sut.StartAnalysis();

        var actualIsStarted = _sut.IsAnalysisStarted;

        await _commentsObserverMock.RaiseAsync(a => a.OnNewInfoEvent += null, badComment);

        await Task.Delay(1000);

        await _commentsObserverMock.RaiseAsync(a => a.OnNewInfoEvent += null, validComment);

        _sut.StopAnalysis();

        // Assert
        Assert.Equal(expectedIsStarted, actualIsStarted);

        _artificialIntelligenceModelFactoryMock
            .Verify(x => x.CreateArtificialIntelligenceModel(modelKey), Times.Exactly(2));

        _commentsObserverMock
            .Verify(x => x.StartObserving(), Times.Once);

        _commentsObserverMock
            .Verify(x => x.StopObserving(), Times.Once);

        _evaluatedCommentsRepositoryMock
            .Verify(x => x.Add(It.Is<EvaluatedComment>(x =>
                x.CommentId == evaluatedComment.CommentId &&
                x.EvaluateCategory == evaluatedComment.EvaluateCategory &&
                Math.Abs(x.EvaluateProbability - evaluatedComment.EvaluateProbability) < 0.01)), Times.Once);

        _artificialIntelligenceModelMock
            .Verify(x => x.Predict(text), Times.Once);

        _artificialIntelligenceModelMock
            .Verify(x => x.Dispose(), Times.Exactly(2));
    }
}