using DataCollectionService.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DataCollectionService.Infrastructure.Tests;

public class VkApiTests : IClassFixture<VkApiFixture>
{
    private readonly VkApiFixture _vkApiFixture;

    public VkApiTests(VkApiFixture vkApiFixture)
    {
        _vkApiFixture = vkApiFixture;
    }

    [Fact]
    public async Task GetCommentsCountAsyncTest_Successfull()
    {
        // Arrange

        const int expectedCommentsCount = 9;

        var vkSection = ConfigurationProvider
            .GetConfiguration()
            .GetSection("VkSettings");

        var testCommunityIdStr = vkSection
            .GetSection("Communities")
            .Get<List<string>>()!
            .First();

        var testCommunityId = long.Parse(testCommunityIdStr);

        var testPostId = long.Parse(vkSection["TestPostId"]!);

        // Act

        var actualCommentsCount = await _vkApiFixture.Sut.GetCommentsCountAsync(testCommunityId, testPostId);

        // Assert

        Assert.Equal(expectedCommentsCount, actualCommentsCount);
    }

    [Fact]
    public async Task GetCommentsCountAsyncTest_Exception()
    {
        // Arrange
        const long badTestCommunityId = 0;
        const long badTestPostId = -412421;

        // Act, Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _vkApiFixture.Sut.GetCommentsCountAsync(badTestCommunityId, badTestPostId));
    }

    [Fact]
    public async Task GetCommentsCountAsyncTest_NotFound()
    {
        // Arrange
        const long badTestCommunityId = -451;
        const long badTestPostId = 1421;

        // Act, Assert
        await Assert.ThrowsAsync<Exception>(async () => await _vkApiFixture.Sut.GetCommentsCountAsync(badTestCommunityId, badTestPostId));
    }

    [Fact]
    public async Task GetLastPostIdAsync_Success()
    {
        // Arrange

        var vkSection = ConfigurationProvider
            .GetConfiguration()
            .GetSection("VkSettings");

        var testCommunityIdStr = vkSection
            .GetSection("Communities")
            .Get<List<string>>()!
            .First();

        var testCommunityId = long.Parse(testCommunityIdStr);

        var expectedPostId = long.Parse(vkSection["TestPostId"]!);

        // Act

        var actualPostId = await _vkApiFixture.Sut.GetLastPostIdAsync(testCommunityId);

        // Assert

        Assert.Equal(expectedPostId, actualPostId);
    }

    [Fact]
    public async Task GetLastPostIdAsync_NotFound()
    {
        // Arrange
        const long badTestCommunityId = -40;

        // Act, Assert
        await Assert.ThrowsAsync<Exception>(async () => await _vkApiFixture.Sut.GetLastPostIdAsync(badTestCommunityId));
    }

    [Fact]
    public async Task GetCommentsAsync_Equal_4()
    {
        // Arrange
        const int expectedCommentsCount = 4;

        var vkSection = ConfigurationProvider
            .GetConfiguration()
            .GetSection("VkSettings");

        var testCommunityIdStr = vkSection
            .GetSection("Communities")
            .Get<List<string>>()!
            .First();

        var testCommunityId = long.Parse(testCommunityIdStr);

        var testPostId = long.Parse(vkSection["TestPostId"]!);

        // Act
        var comments = (await _vkApiFixture.Sut.GetCommentsAsync(testCommunityId, testPostId, 100, 0)).ToList();

        // Assert
        Assert.NotEmpty(comments);
        Assert.Equal(expectedCommentsCount, comments.Count);
    }

    [Fact]
    public async Task GetCommentsAsync_Branch1()
    {
        // Arrange
        const int expectedCommentsCount = 3;

        var vkSection = ConfigurationProvider
            .GetConfiguration()
            .GetSection("VkSettings");

        var testCommunityIdStr = vkSection
            .GetSection("Communities")
            .Get<List<string>>()!
            .First();

        var testCommunityId = long.Parse(testCommunityIdStr);

        var testPostId = long.Parse(vkSection["TestPostId"]!);

        var testBranchId = int.Parse(vkSection["TestBranchId"]!);

        // Act
        var comments = (await _vkApiFixture.Sut.GetCommentsAsync(testCommunityId, testPostId, 100, 0, testBranchId)).ToList();

        // Assert
        Assert.NotEmpty(comments);
        Assert.Equal(expectedCommentsCount, comments.Count);
    }

    [Fact]
    public async Task GetCommentsAsync_Branch2()
    {
        // Arrange
        const int expectedCommentsCount = 2;

        var vkSection = ConfigurationProvider
            .GetConfiguration()
            .GetSection("VkSettings");

        var testCommunityIdStr = vkSection
            .GetSection("Communities")
            .Get<List<string>>()!
            .First();

        var testCommunityId = long.Parse(testCommunityIdStr);

        var testPostId = long.Parse(vkSection["TestPostId"]!);

        var testBranchId = int.Parse(vkSection["TestBranchId1"]!);

        // Act
        var comments = (await _vkApiFixture.Sut.GetCommentsAsync(testCommunityId, testPostId, 100, 0, testBranchId)).ToList();

        // Assert
        Assert.NotEmpty(comments);
        Assert.Equal(expectedCommentsCount, comments.Count);
    }
}

public class VkApiFixture : IAsyncDisposable
{
    public readonly VkApi Sut;

    public VkApiFixture()
    {
        Sut = new VkApi();

        var vkSettings = ConfigurationProvider.GetConfiguration().GetSection("VkSettings");
        Sut.LogInAsync(new VkApiCredentials
        {
            ApplicationId = ulong.Parse(vkSettings["ApplicationId"]!),
            SecureKey = vkSettings["SecureKey"]!,
            ServiceAccessKey = vkSettings["ServiceAccessKey"]!
        }).GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        await Sut.LogOutAsync();
    }
}