using System.Data;
using System.Data.SqlClient;

namespace Common;

public abstract class MsSqlServerClient : IDatabaseClient
{
    protected readonly SqlConnection Connection;
    protected MsSqlServerClient(string connectionString) => Connection = new SqlConnection(connectionString);
    public void Connect()
    {
        if(Connection.State is ConnectionState.Open or ConnectionState.Connecting) return;
        Connection.Open();
    }
    public void Disconnect()
    {
        if (Connection.State == ConnectionState.Closed) return;
        Connection.Close();
    }

    public abstract void Add<T>(T result);
    public abstract List<T> GetRange<T>(int startIndex);
    public abstract void Clear();

    protected void SafeAccess(Action accessAction)
    {
        int attempts;
        const int retryDelayMs = 5000;
        for (attempts = 0; attempts < 5; attempts++)
        {
            try
            {
                accessAction.Invoke();
                break;
            }
            catch (Exception e)
            {
                Connection.Close();
                Connection.Open();
                Logger.Write($"{e.Message} {e.StackTrace}");
                Thread.Sleep(retryDelayMs);
            }
        }
        if (attempts == 3) throw new Exception("Number of attempts to access to database was exceeded");
    }
}