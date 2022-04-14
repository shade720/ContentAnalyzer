using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class Database
{
    protected readonly AppContext Context;
    protected Database(string connectionString)
    {
        var options = new DbContextOptionsBuilder<AppContext>().UseSqlServer(connectionString).Options;
        Context = new AppContext(options);
        Context.Database.EnsureCreated();
    } 
    public void Connect()
    {
        Context.Database.OpenConnection();
    }
    public void Disconnect()
    {
        Context.Database.CloseConnection();
    }
}