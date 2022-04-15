using Common;
using Common.EntityFramework;

namespace DataCollectionService.DatabaseClients;

public class AllCommentsDb : DatabaseClient
{
    public AllCommentsDb(string connectionString) : base(connectionString) { }
    public override void Add<T>(T commentData)
    {
        var dataFrame = commentData as CommentData;
        if (IsDataFrameInvalid(dataFrame)) return;
        Context.Comments.Add(dataFrame);
        Context.SaveChanges();
    }

    public override List<T> GetRange<T>(int startIndex)
    {
        var result = from c in Context.Comments where c.Id > startIndex select c;
        return result as List<T> ?? new List<T>();
    }

    public override void Clear()
    {
        Context.Comments.RemoveRange(Context.Comments.ToList());
        Context.SaveChanges();
    }

    private static bool IsDataFrameInvalid(CommentData dataFrame)
    {
        return string.IsNullOrEmpty(dataFrame.Text) || string.IsNullOrWhiteSpace(dataFrame.Text) || dataFrame.Text.Length < 5;
    }
}