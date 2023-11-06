using Common.EntityFramework;
using Common.SharedDomain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAnalysisService.Infrastructure.Tests;

public class CommentsRepositoryTests : IClassFixture<TestDatabaseFixture>
{
    private readonly CommentsRepository _sut;

    public CommentsRepositoryTests(TestDatabaseFixture fixture)
    {
        _sut = new CommentsRepository(fixture.CreateDbContextFactory());
    }

    [Fact]
    public async Task GetRange_Return_Only_NotAnalyzed()
    {
        // Arrange
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
        };
        
        // Act
        var actualComments = (await _sut.GetRange(null)).ToList();

        // Assert
        Assert.Equal(expectedComments, actualComments, new CommentsEqualityComparer());
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

public class TestDatabaseFixture
{
    public class CommentsContextFactory : IDbContextFactory<CommentsContext>
    {
        public CommentsContext CreateDbContext()
        {
            return CreateCommentsContext();
        }
    }

    private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerTestDatabase;Integrated Security=True;";

    public CommentsContext CreateContext()
    {
        return CreateCommentsContext();
    }

    public IDbContextFactory<CommentsContext> CreateDbContextFactory()
    {
        return new CommentsContextFactory();
    }

    private static CommentsContext CreateCommentsContext()
    {
        return new CommentsContext(
            new DbContextOptionsBuilder<CommentsContext>()
                .UseSqlServer(ConnectionString)
                .Options);
    }
}