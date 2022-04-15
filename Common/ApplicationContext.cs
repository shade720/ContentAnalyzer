using Microsoft.EntityFrameworkCore;

namespace Common;

public class ApplicationContext : DbContext
{
    public DbSet<CommentData> Comments { get; set; }
    public DbSet<EvaluateResult> EvaluateResults { get; set; }

    public ApplicationContext() { }

    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
    }
}