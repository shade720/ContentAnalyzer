using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataCollectionService.DatabaseClients;

public class CommentsDatabaseClient : DatabaseClient<Comment>
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    public CommentsDatabaseClient(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    public override void Add(Comment comment)
    {
        if (IsDataFrameInvalid(comment)) return;
        using var context = _contextFactory.CreateDbContext();
        if (context.Comments.Any(x => x.CommentId == comment.CommentId)) return;
        context.Comments.Add(comment);
        context.SaveChanges();
        Log.Logger.Information("{0} comments collected", context.Comments.Count());
    }

    public override List<Comment> GetRange(CommentsQueryFilter filter)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Comments.Where(c => c.Id > filter.Id).ToList();
    }

    public override void Clear()
    {
        using var context = _contextFactory.CreateDbContext();
        context.Comments.RemoveRange(context.Comments.ToList());
        context.SaveChanges();
    }

    private static bool IsDataFrameInvalid(Comment frame)
    {
        return string.IsNullOrEmpty(frame.Text) || string.IsNullOrWhiteSpace(frame.Text) || frame.Text.Length < 5;
    }
}