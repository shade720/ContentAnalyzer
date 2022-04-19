using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DataAnalysisService.DatabaseClients;

public class SuspiciousCommentsDb : DatabaseClient<EvaluateResult>
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    public SuspiciousCommentsDb(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public override void Add(EvaluateResult result)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Comments.Single(comment => comment.Id == result.CommentDataId).EvaluateResults.Add(result);
        context.SaveChanges();
    }

    public override GetRangeResult GetRange(int startIndex)
    {
        using var context = _contextFactory.CreateDbContext();
        var queryResult =
            context.EvaluateResults
            .Include(comment => comment.CommentData)
            .Where(evaluateResult => evaluateResult.Id > startIndex);
        return new GetRangeResult(queryResult.ToList());
    }

    public override void Clear()
    {
        using var context = _contextFactory.CreateDbContext();
        context.EvaluateResults.RemoveRange(context.EvaluateResults.ToList());
        context.SaveChanges();
    }
}