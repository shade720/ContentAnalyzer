using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.DatabaseClients;

public class AllCommentsDb : DatabaseClient<CommentData>
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    public AllCommentsDb(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    public override void Add(CommentData commentData)
    {
        if (IsDataFrameInvalid(commentData)) return;
        using var context = _contextFactory.CreateDbContext();
        if (context.Comments.Contains(commentData)) return;
        context.Comments.Add(commentData);
        context.SaveChanges();
    }

    public override GetRangeResult GetRange(int startIndex)
    {
        using var context = _contextFactory.CreateDbContext();
        var queryResult = context.Comments.Where(c => c.Id > startIndex);
        return new GetRangeResult(queryResult.ToList());
    }

    public override void Clear()
    {
        using var context = _contextFactory.CreateDbContext();
        context.Comments.RemoveRange(context.Comments.ToList());
        context.SaveChanges();
    }

    private static bool IsDataFrameInvalid(CommentData dataFrame)
    {
        return string.IsNullOrEmpty(dataFrame.Text) || string.IsNullOrWhiteSpace(dataFrame.Text) || dataFrame.Text.Length < 5;
    }
}