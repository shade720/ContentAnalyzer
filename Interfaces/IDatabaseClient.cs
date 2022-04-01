namespace Common;

public interface IDatabaseClient
{
    public void Connect();
    public void Disconnect();
    public void Add<T>(T result);
    public List<T> GetRange<T>(int startIndex);
    public void Clear();
}