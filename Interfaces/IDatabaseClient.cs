namespace Interfaces;

public interface IDatabaseClient
{
    public void Connect();
    public void Disconnect();
    public void Add<T>(T result);
    public void Clear();
}