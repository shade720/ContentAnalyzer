using Common;

namespace DataAnalysisService.DatabaseClients;

public class SuspiciousCommentsDb : DatabaseClient
{
    public SuspiciousCommentsDb(string connectionString) : base(connectionString) { }
    public override void Add<T>(T result)
    {
        if (result is not EvaluateResult evaluateResult) return;
        Context.EvaluateResults.Add(evaluateResult);
        Context.SaveChanges();
    }

    public override List<T> GetRange<T>(int startIndex)
    {
        var result = from c in Context.EvaluateResults where c.CommentData.Id > startIndex select c;
        return result as List<T> ?? new List<T>();
    }

    public override void Clear()
    {
        Context.EvaluateResults.RemoveRange(Context.EvaluateResults.ToList());
    }
}