using Common;
using Microsoft.EntityFrameworkCore;

namespace DataAnalysisService.DatabaseClients;

public class SuspiciousCommentsDb : DatabaseClient
{
    public SuspiciousCommentsDb(string connectionString) : base(connectionString) { }
    public override void Add<T>(T result)
    {
        if (result is not EvaluateResult evaluateResult) return;
        var parent = from comment in Context.Comments where comment.Id == evaluateResult.CommentDataId select comment;
        parent.First().EvaluateResults.Add(evaluateResult);
        Context.SaveChanges();
    }

    public override List<T> GetRange<T>(int startIndex)
    {
        var result = 
            from c in Context.EvaluateResults
                .Include(evaluateResult => from commentData in Context.Comments where evaluateResult.CommentDataId == commentData.Id select commentData) 
            where c.CommentDataId > startIndex select c;
        return result as List<T> ?? new List<T>();
    }

    public override void Clear()
    {
        Context.EvaluateResults.RemoveRange(Context.EvaluateResults.ToList());
        Context.SaveChanges();
    }
}