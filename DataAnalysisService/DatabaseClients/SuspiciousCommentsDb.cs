using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DataAnalysisService.DatabaseClients;

public class SuspiciousCommentsDb : DatabaseClient
{
    public SuspiciousCommentsDb(DbContextOptions<CommentsContext> options) : base(options) { }

    public override void Add<T>(T result)
    {
        if (result is not EvaluateResult evaluateResult) return;
        Context.Comments.Single(comment => comment.Id == evaluateResult.CommentDataId).EvaluateResults.Add(evaluateResult);
        Context.SaveChanges();
    }

    public override List<T> GetRange<T>(int startIndex)
    {
        var result = from evaluateResult in Context.EvaluateResults.Include(comment => comment.CommentData) where evaluateResult.Id > startIndex select evaluateResult;
        return result as List<T> ?? new List<T>();
    }

    public override void Clear()
    {
        Context.EvaluateResults.RemoveRange(Context.EvaluateResults.ToList());
        Context.SaveChanges();
    }
}