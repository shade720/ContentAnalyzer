using Microsoft.EntityFrameworkCore;

namespace Common;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }

    public DbSet<CommentData> Comments { get; set; }
    public DbSet<EvaluateResult> EvaluateResults { get; set; }
}