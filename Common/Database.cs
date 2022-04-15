using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class Database
{
    private readonly DbContextOptions<ApplicationContext> _options;
    protected ApplicationContext Context;

    protected Database(string connectionString)
    {
        _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;")
            .Options;
    } 
    public void Connect()
    {
        Context = new ApplicationContext(_options);
        Context.Database.EnsureCreated();
    }
    public void Disconnect()
    {
        Context.Dispose();
    }
}