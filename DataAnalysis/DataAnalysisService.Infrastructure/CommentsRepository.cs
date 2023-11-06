using Common.EntityFramework;
using Common.SharedDomain;
using Microsoft.EntityFrameworkCore;

namespace DataAnalysisService.Infrastructure;

public class CommentsRepository : ICommentsRepository
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;

    public CommentsRepository(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Task Add(Comment comment)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<Comment>> GetRange(CommentsQueryFilter? filter)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var filteredComments = context.Comments.Where(c => !c.IncludedInEvaluatedComments.Any()).ToList();
        return filteredComments.AsQueryable();
    }

    public Task Clear()
    {
        throw new NotImplementedException();
    }
}