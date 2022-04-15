using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class Database
{
    private readonly DbContextOptions<CommentsContext> _options;
    protected CommentsContext Context;

    protected Database(DbContextOptions<CommentsContext> options)
    {
        _options = options;
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