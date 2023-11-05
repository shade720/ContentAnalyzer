using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace DataCollectionService.Application.Tests;

public class VkCollectorTests
{
    private readonly Mock<IVkApi> _vkApiMock;

    private readonly VkCollector _sut;

    public VkCollectorTests()
    {
        _vkApiMock = new Mock<IVkApi>();
        _sut = new VkCollector(_vkApiMock.Object, ConfigurationProvider.GetConfiguration());
    }

    [Fact]
    public async Task VkCollector_Test()
    {
        // Arrange
        const long fakeCommunityId = -451;
        const long fakePostId = 1;
        var scanCommentsDelay = ConfigurationProvider.GetConfiguration()["ScanCommentsDelay"];
        var scanPostDelay = ConfigurationProvider.GetConfiguration()["ScanPostDelay"];
        var fakeComment = new VkComment
        {
            CommentId = 1,
            AuthorId = 1,
            GroupId = fakeCommunityId,
            PostId = fakePostId,
            PostDate = DateTime.Now,
            Text = "Тестовый комментарий №1",
            ThreadCommentsCount = 0
        };

        _vkApiMock
            .Setup(x => x.GetLastPostIdAsync(fakeCommunityId))
            .ReturnsAsync(fakePostId);

        _vkApiMock
            .Setup(x => x.GetCommentsCountAsync(fakeCommunityId, fakePostId))
            .ReturnsAsync(1);

        _vkApiMock
            .Setup(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, null))
            .ReturnsAsync(new List<VkComment> { fakeComment });

        var expectedComments = new List<VkComment>();

        // Act
        _sut.OnCommentCollectedEvent += comment =>
        {
            expectedComments.Add(comment);
            return Task.CompletedTask;
        };

        _sut.StartCollecting();

        var postObserversCountBeforeStopping = _sut.PostObservers.Count;

        await Task.Delay(int.Parse(scanPostDelay) + 100);

        var commentsObserversCountBeforeStopping = _sut.CommentsObservers.Count;

        await Task.Delay(int.Parse(scanCommentsDelay) + 100);

        _sut.StopCollecting();

        var postObserversCountAfterStopping = _sut.PostObservers.Count;
        var commentsObserversCountAfterStopping = _sut.CommentsObservers.Count;

        // Assert

        Assert.Equal(1, postObserversCountBeforeStopping);
        Assert.Equal(1, commentsObserversCountBeforeStopping);

        Assert.Equal(0, postObserversCountAfterStopping);
        Assert.Equal(0, commentsObserversCountAfterStopping);

        Assert.Single(expectedComments);

        Assert.Equal(fakeComment, expectedComments.Single());

        _vkApiMock.Verify(x => x.GetLastPostIdAsync(fakeCommunityId), Times.Exactly(3));
        _vkApiMock.Verify(x => x.GetCommentsCountAsync(fakeCommunityId, fakePostId), Times.Once);
        _vkApiMock.Verify(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, null), Times.Once);
    }
}