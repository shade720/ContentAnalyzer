using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class DatabaseClient : Database
{
    protected DatabaseClient(DbContextOptions<CommentsContext> options) : base(options) { }
    public abstract void Add<T>(T result);
    public abstract List<T> GetRange<T>(int startIndex);
    public abstract void Clear();
}