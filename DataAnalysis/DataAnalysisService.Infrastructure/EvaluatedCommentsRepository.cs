using Common.EntityFramework;
using Common.SharedDomain;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAnalysisService.Infrastructure;

public class EvaluatedCommentsRepository : IEvaluatedCommentsRepository
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;

    public EvaluatedCommentsRepository(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task Add(EvaluatedComment result)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        (await context.Comments.SingleAsync(comment => comment.Id == result.CommentId))
            .IncludedInEvaluatedComments.Add(result);
        await context.SaveChangesAsync();
        Log.Logger.Information("{0} comments evaluated", context.EvaluatedComments.Count());
    }

    public async Task<List<EvaluatedComment>> GetRange(CommentsQueryFilter filter)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return context.EvaluatedComments
                .Include(comment => comment.RelatedComment)
                .Where(c => filter.AuthorId <= 0 || c.RelatedComment.AuthorId == filter.AuthorId)
                .Where(c => filter.PostId <= 0 || c.RelatedComment.PostId == filter.PostId)
                .Where(c => filter.GroupId <= 0 || c.RelatedComment.GroupId == filter.GroupId)
                .Where(c => filter.FromDate.Year <= 1970 || c.RelatedComment.PostDate > filter.FromDate)
                .Where(c => filter.ToDate.Year <= 1970 || c.RelatedComment.PostDate < filter.ToDate)
                .Where(c => string.IsNullOrEmpty(filter.Text) || c.RelatedComment.Text.Contains(filter.Text))
                .Where(c => string.IsNullOrEmpty(filter.Category) || c.EvaluateCategory.Contains(filter.Category))
                .ToList();
    }

    public async Task Clear()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        await context.EvaluatedComments.ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}