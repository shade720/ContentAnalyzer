using Microsoft.EntityFrameworkCore;

namespace Common.EntityFramework;

public sealed class CommentsContext : DbContext
{
    public DbSet<CommentData> Comments { get; set; }
    public DbSet<EvaluateResult> EvaluateResults { get; set; }

    public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}