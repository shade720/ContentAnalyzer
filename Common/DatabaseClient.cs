using Common.EntityFramework;

namespace Common;

public abstract class DatabaseClient<T>
{
    public abstract void Add(T result);
    public abstract IQueryable<T> GetRange(int startIndex);
    public abstract void Clear();
}