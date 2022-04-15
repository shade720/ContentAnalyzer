using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class Database
{
    private readonly DbContextOptions<CommentsContext> _options;
    protected CommentsContext Context;

    protected Database(string connectionString)
    {
        _options = new DbContextOptionsBuilder<CommentsContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;")
            .Options;
    } 
    public void Connect()
    {
        Context = new CommentsContext(_options);
        Context.Database.EnsureCreated();
    }
    public void Disconnect()
    {
        Context.Dispose();
    }
}