namespace Common;

public abstract class DatabaseClient : Database
{
    protected DatabaseClient(string connectionString) : base(connectionString)
    {
    }
    public abstract void Add<T>(T result);
    public abstract List<T> GetRange<T>(int startIndex);
    public abstract void Clear();
}