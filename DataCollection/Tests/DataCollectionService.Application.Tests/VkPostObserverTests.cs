using DataCollectionService.Application.VkObservers;
using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace DataCollectionService.Application.Tests;

public class VkPostObserverTests
{
    private readonly Mock<IVkApi> _vkApiMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public VkPostObserverTests()
    {
        _vkApiMock = new Mock<IVkApi>();
        _configurationMock = new Mock<IConfiguration>();
    }

    [Fact]
    public async Task VkPostObserving_Success()
    {
        // Arrange
        const long fakeCommunityId = -412;
        const long expectedPostId = 1;
        const string waitTimeStr = "5000";

        _vkApiMock
            .Setup(x => x.GetLastPostIdAsync(fakeCommunityId))
            .ReturnsAsync(expectedPostId);

        _configurationMock
            .Setup(x => x["ScanPostDelay"])
            .Returns(waitTimeStr);


        // Act
        var sut = new VkPostObserver(fakeCommunityId, _vkApiMock.Object, _configurationMock.Object);

        // Assert
        var eventData = await Assert.RaisesAsync<long>(
            h => sut.OnNewInfoEvent += h,
            h => sut.OnNewInfoEvent -= h,
            async () => await Task.Delay(int.Parse(waitTimeStr)));

        Assert.Equal(expectedPostId, eventData.Arguments);

        // Teardown
        sut.Dispose();
    }

    [Fact]
    public async Task VkPostObserving_AlreadyPresent()
    {
        // Arrange
        const long fakeCommunityId = -412;
        const long expectedPostId = 1;
        const long expectedEventsCount = 1;

        const string delayBetweenEvents = "2000";
        const int timeFor2Events = 4000;

        var actualEvents = new List<long>();

        _vkApiMock
            .Setup(x => x.GetLastPostIdAsync(fakeCommunityId))
            .ReturnsAsync(expectedPostId);

        _configurationMock
            .Setup(x => x["ScanPostDelay"])
            .Returns(delayBetweenEvents);

        // Act
        using (var sut = new VkPostObserver(fakeCommunityId, _vkApiMock.Object, _configurationMock.Object))
        {
            sut.OnNewInfoEvent += (_, l) => 
                actualEvents.Add(l);
            await Task.Delay(timeFor2Events);
        }

        // Assert
        Assert.Equal(expectedEventsCount, actualEvents.Count);
        _vkApiMock.Verify(x => x.GetLastPostIdAsync(fakeCommunityId), Times.Exactly(2));
    }
}