namespace Common;

public abstract class MsSqlServerClient : MsSqlServer
{
    protected MsSqlServerClient(string connectionString) : base(connectionString)
    {
    }

    public abstract void Add<T>(T result);
    public abstract List<T> GetRange<T>(int startIndex);
    public abstract void Clear();
}