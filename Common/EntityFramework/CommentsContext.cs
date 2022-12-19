using Microsoft.EntityFrameworkCore;

namespace Common.EntityFramework;

public sealed class CommentsContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<EvaluatedComment> EvaluatedComments { get; set; }

    public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}