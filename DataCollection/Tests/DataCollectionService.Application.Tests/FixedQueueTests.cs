using Xunit;

namespace DataCollectionService.Application.Tests;

public class FixedQueueTests
{
    private const int Limit = 3;
    private readonly FixedQueue<int> _sut;

    public FixedQueueTests()
    {
        _sut = new FixedQueue<int>(Limit);
    }

    [Fact]
    public void Enqueue_Test_Limit()
    {
        // Arrange
        // Act
        foreach (var i in Enumerable.Range(0, Limit + 1))
            _sut.Enqueue(i);
        
        // Assert
        Assert.Equal(Limit, _sut.Count);
        Assert.DoesNotContain(0, _sut);
    }

    [Fact]
    public void Clear_Test()
    {
        // Arrange
        // Act
        foreach (var i in Enumerable.Range(0, Limit + 1))
            _sut.Enqueue(i);
        _sut.Clear();

        // Assert
        Assert.Equal(0, _sut.Count);
    }

    [Fact]
    public void Dequeue_Test()
    {
        // Arrange
        const int expectedDequeuedElement = 1;

        // Act
        foreach (var i in Enumerable.Range(0, Limit + 1))
            _sut.Enqueue(i);
        var actualDequeuedElement = _sut.Dequeue();

        // Assert
        Assert.Equal(Limit - 1, _sut.Count);
        Assert.Equal(expectedDequeuedElement, actualDequeuedElement);
    }
}