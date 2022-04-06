using System.Data;
using System.Data.SqlClient;

namespace Common;

public abstract class MsSqlServer
{
    protected readonly SqlConnection Connection;
    protected MsSqlServer(string connectionString) => Connection = new SqlConnection(connectionString);
    public void Connect()
    {
        if (Connection.State is ConnectionState.Open or ConnectionState.Connecting)
        {
            Logger.Log("Database already connected", Logger.LogLevel.Error);
            return;
        }
        Connection.Open();
    }
    public void Disconnect()
    {
        if (Connection.State == ConnectionState.Closed)
        {
            Logger.Log("Database already disconnected", Logger.LogLevel.Error);
            return;
        }
        Connection.Close();
    }
    protected static void SafeAccess(Action accessAction)
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
                Logger.Log($"{e.Message} {e.StackTrace}", Logger.LogLevel.Error);
                Thread.Sleep(retryDelayMs);
            }
        }
        if (attempts == 5) throw new Exception("Number of attempts to access to database was exceeded");
    }
}