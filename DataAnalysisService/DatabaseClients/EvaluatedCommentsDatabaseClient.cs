using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAnalysisService.DatabaseClients;

public class EvaluatedCommentsDatabaseClient : DatabaseClient<Common.EntityFramework.EvaluatedComment>
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    public EvaluatedCommentsDatabaseClient(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public override void Add(Common.EntityFramework.EvaluatedComment result)
    {
        using var context = _contextFactory.CreateDbContext();
        if (context.EvaluatedComments.Any(x => x.CommentId == result.CommentId)) return;
        context.Comments.Single(comment => comment.Id == result.CommentId).IncludedInEvaluatedComments.Add(result);
        context.SaveChanges();
        Log.Logger.Information("{0} comments evaluated", context.EvaluatedComments.Count());
    }

    public override IQueryable<Common.EntityFramework.EvaluatedComment> GetRange(int startIndex)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.EvaluatedComments
            .Include(comment => comment.RelatedComment)
            .Where(evaluateResult => evaluateResult.Id > startIndex);
    }

    public override void Clear()
    {
        using var context = _contextFactory.CreateDbContext();
        context.EvaluatedComments.RemoveRange(context.EvaluatedComments.ToList());
        context.SaveChanges();
    }
}