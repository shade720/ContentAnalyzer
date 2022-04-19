using Microsoft.EntityFrameworkCore;

namespace Common.EntityFramework;

public class CommentsContext : DbContext
{
    public DbSet<CommentData> Comments { get; set; }
    public DbSet<EvaluateResult> EvaluateResults { get; set; }

    //public CommentsContext() { }

    public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
    //}
}