using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.DatabaseClients;

public class AllCommentsDb : DatabaseClient<CommentData>
{
    public AllCommentsDb(DbContextOptions<CommentsContext> options) : base(options) { }
    public override void Add(CommentData commentData)
    {
        var dataFrame = commentData as CommentData;
        if (IsDataFrameInvalid(dataFrame)) return;
        Context.Comments.Add(dataFrame);
        Context.SaveChanges();
    }

    public override GetRangeResult GetRange(int startIndex)
    {
        var queryResult = Context.Comments.Where(c => c.Id > startIndex);
        return new GetRangeResult {Result = queryResult.ToList()};
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