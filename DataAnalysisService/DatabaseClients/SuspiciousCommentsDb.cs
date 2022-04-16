using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DataAnalysisService.DatabaseClients;

public class SuspiciousCommentsDb : DatabaseClient<EvaluateResult>
{
    public SuspiciousCommentsDb(DbContextOptions<CommentsContext> options) : base(options) { }

    public override void Add(EvaluateResult result)
    {
        if (result is not EvaluateResult evaluateResult) return;
        Context.Comments.Single(comment => comment.Id == evaluateResult.CommentDataId).EvaluateResults.Add(evaluateResult);
        Context.SaveChanges();
    }

    public override GetRangeResult GetRange(int startIndex)
    {
        var queryResult = 
            Context.EvaluateResults
            .Include(comment => comment.CommentData)
            .Where(evaluateResult => evaluateResult.Id > startIndex);
        return new GetRangeResult { Result = queryResult.ToList() };
    }

    public override void Clear()
    {
        Context.EvaluateResults.RemoveRange(Context.EvaluateResults.ToList());
        Context.SaveChanges();
    }
}