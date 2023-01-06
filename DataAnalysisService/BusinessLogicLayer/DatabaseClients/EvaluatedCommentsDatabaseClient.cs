using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAnalysisService.BusinessLogicLayer.DatabaseClients;

public class EvaluatedCommentsDatabaseClient : DatabaseClient<EvaluatedComment>
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;
    public EvaluatedCommentsDatabaseClient(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public override void Add(EvaluatedComment result)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Comments.Single(comment => comment.Id == result.CommentId).IncludedInEvaluatedComments.Add(result);
        context.SaveChanges();
        Log.Logger.Information("{0} comments evaluated", context.EvaluatedComments.Count());
    }

    public override IQueryable<EvaluatedComment> GetRange(CommentsQueryFilter filter)
    { using var context = _contextFactory.CreateDbContext();
        return context.EvaluatedComments
                .Include(comment => comment.RelatedComment)
                .AsParallel()
                .Where(c => filter.AuthorId <= 0 || c.RelatedComment.AuthorId == filter.AuthorId)
                .Where(c => filter.PostId <= 0 || c.RelatedComment.PostId == filter.PostId)
                .Where(c => filter.GroupId <= 0 || c.RelatedComment.GroupId == filter.GroupId)
                .Where(c => filter.FromDate.Year <= 1970 || c.RelatedComment.PostDate > filter.FromDate)
                .Where(c => filter.ToDate.Year <= 1970 || c.RelatedComment.PostDate < filter.ToDate)
                .AsQueryable();
    }

    public override void Clear()
    {
        using var context = _contextFactory.CreateDbContext();
        context.EvaluatedComments.ExecuteDelete();
        context.SaveChanges();
    }
}