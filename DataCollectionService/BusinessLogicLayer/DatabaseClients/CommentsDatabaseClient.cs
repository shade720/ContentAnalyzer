using System.Text.RegularExpressions;
using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataCollectionService.BusinessLogicLayer.DatabaseClients;

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
        Log.Logger.Information("Add {0} {1} {2} {3} {4} {5}", comment.CommentId, comment.PostId, comment.GroupId, comment.AuthorId, comment.Text, comment.PostDate);
        Log.Logger.Information("{0} comments collected", context.Comments.Count());
    }

    public override IQueryable<Comment> GetRange(CommentsQueryFilter filter)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Comments.Where(c => c.Id > filter.Id);
    }

    public override void Clear()
    {
        using var context = _contextFactory.CreateDbContext();
        context.Comments.ExecuteDelete();
        context.SaveChanges();
    }

    private static bool IsDataFrameInvalid(Comment frame)
    {
        return string.IsNullOrEmpty(frame.Text) || string.IsNullOrWhiteSpace(frame.Text) || frame.Text.Length < 5;
    }
}