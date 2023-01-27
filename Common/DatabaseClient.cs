namespace Common;

public abstract class DatabaseClient<T>
{
    public abstract void Add(T result);
    public abstract List<T> GetRange(CommentsQueryFilter filter);
    public abstract void Clear();
}