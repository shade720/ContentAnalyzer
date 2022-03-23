using System.Data;
using System.Data.SqlClient;

namespace Interfaces;

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
    public abstract void Clear();
    protected void SafeAccess(Action accessAction)
    {
        int attempts;
        for (attempts = 0; attempts < 3; attempts++)
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
                Console.WriteLine($"{e.Message} {e.StackTrace}");
            }
        }
        if (attempts == 3) throw new Exception("Number of attempts to access to database was exceeded");
    }
}