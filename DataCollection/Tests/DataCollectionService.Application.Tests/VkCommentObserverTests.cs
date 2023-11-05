using DataCollectionService.Application.VkObservers;
using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace DataCollectionService.Application.Tests;

public class VkCommentObserverTests
{
    private readonly Mock<IVkApi> _vkApiMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public VkCommentObserverTests()
    {
        _vkApiMock = new Mock<IVkApi>();
        _configurationMock = new Mock<IConfiguration>();
    }

    [Fact]
    public async Task VkPostObserving_Success()
    {
        // Arrange
        const long fakeCommunityId = -412;
        const long fakePostId = 1;

        const string waitTimeStr = "1000";

        var expectedMainBranch = new List<VkComment>
        {
            new()
            {
                CommentId = 1,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №1",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 2,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2",
                ThreadCommentsCount = 3
            },
            new()
            {
                CommentId = 3,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №3",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 4,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №4",
                ThreadCommentsCount = 0
            }
        };
        var expectedSecondaryBranch = new List<VkComment>
        {
            new()
            {
                CommentId = 5,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2.1",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 6,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2.2",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 7,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2.3",
                ThreadCommentsCount = 0
            },
        };
        var expectedResultCommentsList = new List<VkComment>
        {
            expectedMainBranch[0],
            expectedMainBranch[1],
            expectedSecondaryBranch[0],
            expectedSecondaryBranch[1],
            expectedSecondaryBranch[2],
            expectedMainBranch[2],
            expectedMainBranch[3],
        };

        var receivedComments = new List<VkComment>();

        _vkApiMock
            .Setup(x => x.GetCommentsCountAsync(fakeCommunityId, fakePostId))
            .ReturnsAsync(expectedMainBranch.Count + expectedSecondaryBranch.Count);

        _vkApiMock
            .Setup(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, null))
            .ReturnsAsync(expectedMainBranch);

        _vkApiMock
            .Setup(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, 2))
            .ReturnsAsync(expectedSecondaryBranch);

        _configurationMock
            .Setup(x => x["ScanCommentsDelay"])
            .Returns(waitTimeStr);

        // Act
        using (var sut = new VkCommentObserver(fakeCommunityId, fakePostId, _vkApiMock.Object, _configurationMock.Object))
        {
            sut.OnNewInfoEvent += (_, comment) =>
                receivedComments.Add(comment);
            await Task.Delay(int.Parse(waitTimeStr) + 500);
        }
        
        // Assert
        Assert.Equal(expectedResultCommentsList, receivedComments);
        _vkApiMock.Verify(x => x.GetCommentsCountAsync(fakeCommunityId, fakePostId), Times.Once);
        _vkApiMock.Verify(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, null), Times.Once);
        _vkApiMock.Verify(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, 2), Times.Once);
    }

    [Fact]
    public async Task VkPostObserving_AlreadyPresent()
    {
        // Arrange
        const long fakeCommunityId = -412;
        const long fakePostId = 1;

        const string waitTimeStr = "1000";
        const int timeForTwoIteration = 2100;

        var expectedMainBranch = new List<VkComment>
        {
            new()
            {
                CommentId = 1,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №1",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 2,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2",
                ThreadCommentsCount = 3
            },
            new()
            {
                CommentId = 3,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №3",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 4,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №4",
                ThreadCommentsCount = 0
            }
        };
        var expectedSecondaryBranch = new List<VkComment>
        {
            new()
            {
                CommentId = 5,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2.1",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 6,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2.2",
                ThreadCommentsCount = 0
            },
            new()
            {
                CommentId = 7,
                AuthorId = 1,
                GroupId = fakeCommunityId,
                PostId = fakePostId,
                PostDate = DateTime.Now,
                Text = "Тестовый комментарий №2.3",
                ThreadCommentsCount = 0
            },
        };
        var expectedResultCommentsList = new List<VkComment>
        {
            expectedMainBranch[0],
            expectedMainBranch[1],
            expectedSecondaryBranch[0],
            expectedSecondaryBranch[1],
            expectedSecondaryBranch[2],
            expectedMainBranch[2],
            expectedMainBranch[3],
        };

        var receivedComments = new List<VkComment>();

        _vkApiMock
            .Setup(x => x.GetCommentsCountAsync(fakeCommunityId, fakePostId))
            .ReturnsAsync(expectedMainBranch.Count + expectedSecondaryBranch.Count);

        _vkApiMock
            .Setup(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, null))
            .ReturnsAsync(expectedMainBranch);

        _vkApiMock
            .Setup(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, 2))
            .ReturnsAsync(expectedSecondaryBranch);

        _configurationMock
            .Setup(x => x["ScanCommentsDelay"])
            .Returns(waitTimeStr);

        // Act
        using (var sut = new VkCommentObserver(fakeCommunityId, fakePostId, _vkApiMock.Object, _configurationMock.Object))
        {
            sut.OnNewInfoEvent += (_, comment) =>
                receivedComments.Add(comment);
            await Task.Delay(timeForTwoIteration);
        }

        // Assert
        Assert.Equal(expectedResultCommentsList, receivedComments);
        _vkApiMock.Verify(x => x.GetCommentsCountAsync(fakeCommunityId, fakePostId), Times.Exactly(2));
        _vkApiMock.Verify(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, null), Times.Once);
        _vkApiMock.Verify(x => x.GetCommentsAsync(fakeCommunityId, fakePostId, 100, 0, 2), Times.Once);
    }
}