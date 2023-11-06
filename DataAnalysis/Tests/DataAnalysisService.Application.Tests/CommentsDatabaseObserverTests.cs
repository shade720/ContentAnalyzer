using Common.SharedDomain;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace DataAnalysisService.Application.Tests;

public class CommentsDatabaseObserverTests
{
    private readonly CommentsDatabaseObserver _sut;
    private readonly Mock<ICommentsRepository> _commentsRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public CommentsDatabaseObserverTests()
    {
        _commentsRepositoryMock = new Mock<ICommentsRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _sut = new CommentsDatabaseObserver(_commentsRepositoryMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task CommentsDatabaseObserving_Success()
    {
        // Arrange
        const int pullPeriod = 5000;

        var expectedComments = new List<Comment>
        {
            new()
            {
                CommentId = 1,
                GroupId = 2,
                PostId = 2,
                AuthorId = 3,
                PostDate = new DateTime(2023,11,06),
                Text = "Тестовый комментарий №1"
            },
            new()
            {
                CommentId = 3,
                GroupId = 2,
                PostId = 2,
                AuthorId = 3,
                PostDate = new DateTime(2023,11,06),
                Text = "Тестовый комментарий №3"
            },
        }.AsQueryable();

        _commentsRepositoryMock
            .Setup(x => x.GetRange(null))
            .ReturnsAsync(expectedComments);

        _configurationMock
            .Setup(x => x["ObserveDelayMs"])
            .Returns(pullPeriod.ToString);

        var actualReceivedComments = new List<Comment>();

        // Act
        _sut.OnNewInfoEvent += async comment =>
        {
            actualReceivedComments.Add(comment);
            await Task.CompletedTask;
        };

        _sut.StartObserving();

        await Task.Delay(pullPeriod + 100);

        _sut.StopObserving();

        // Assert
        Assert.Equal(expectedComments.ToList(), actualReceivedComments.ToList(), new CommentsEqualityComparer());
    }

    [Fact]
    public async Task CommentsDatabaseObserving_StopObservingPrematurely()
    {
        // Arrange
        const int pullPeriod = 5000;
        const int stopObservingPrematurely = 2000;

        _configurationMock
            .Setup(x => x["ObserveDelayMs"])
            .Returns(pullPeriod.ToString);

        var actualReceivedComments = new List<Comment>();

        // Act
        _sut.OnNewInfoEvent += async comment =>
        {
            actualReceivedComments.Add(comment);
            await Task.CompletedTask;
        };

        _sut.StartObserving();

        await Task.Delay(stopObservingPrematurely);

        _sut.StopObserving();

        // Assert
        Assert.Empty(actualReceivedComments);
    }
}

public class CommentsEqualityComparer : IEqualityComparer<List<Comment>>
{
    public bool Equals(List<Comment> x, List<Comment> y)
    {
        for (var i = 0; i < x.Count; i++)
        {
            if (x[i].CommentId != y[i].CommentId ||
                x[i].PostId != y[i].PostId ||
                x[i].GroupId != y[i].GroupId ||
                x[i].AuthorId != y[i].AuthorId ||
                x[i].Text != y[i].Text ||
                x[i].PostDate != y[i].PostDate)
                return false;
        }
        return true;
    }

    public int GetHashCode(List<Comment> obj)
    {
        return HashCode.Combine(obj.Capacity, obj.Count);
    }
}