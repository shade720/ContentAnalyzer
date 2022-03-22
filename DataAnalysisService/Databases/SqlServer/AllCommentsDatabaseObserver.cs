using System.Data.SqlClient;
using Interfaces;

namespace DataAnalysisService.Databases.SqlServer;

public class AllCommentsDatabaseObserver : IDatabaseObserver
{
    private readonly SqlConnection _connection;
    private readonly List<long> _receivedIDs = new();
    private readonly int _observeDelay;
    private CancellationTokenSource _cancellation;

    private delegate void OnDataReceived(IDataFrame data);
    private event OnDataReceived OnDataReceivedEvent;
    private bool _isRunning;

    public AllCommentsDatabaseObserver(string connectionString, int observeDelayMs) => (_connection, _observeDelay) = (new SqlConnection(connectionString), observeDelayMs);

    #region PublicInterface

    public void StartLoading()
    {
        if(_isRunning) return;
        _isRunning = true;
        _connection.Open();
        _cancellation = new CancellationTokenSource();
        var result = LoadingLoop(_cancellation.Token);
    }

    public void StopLoading()
    {
        if(!_isRunning) return;
        _isRunning = false;
        _cancellation.Cancel();
        _connection.Close();
    }

    public void OnDataArrived(Action<IDataFrame> handler)
    {
        OnDataReceivedEvent += handler.Invoke;
    }

    #endregion

    private async Task LoadingLoop(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                LoadData();
                Thread.Sleep(_observeDelay);
            }
        }, cancellationToken);
    }

    private void LoadData()
    {
        SafeAccess(() =>
        {
            using var command = new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date FROM [dbo].[AllComments] ORDER BY Id DESC", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var receivedId = Convert.ToInt64(reader["Id"]);
                if (_receivedIDs.Contains(receivedId)) break;
                _receivedIDs.Add(receivedId);
                OnDataReceivedEvent.Invoke(new DataFrame(
                    Convert.ToInt64(reader["CommentId"]),
                    reader["Content"].ToString() ?? string.Empty,
                    Convert.ToInt64(reader["PostId"]),
                    Convert.ToInt64(reader["GroupId"]),
                    Convert.ToInt64(reader["AuthorId"]),
                    Convert.ToDateTime(reader["Date"])
                ));
            }
        });
    }
    private void SafeAccess(Action accessAction)
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
                _connection.Close();
                _connection.Open();
                Console.WriteLine($"{e.Message} {e.StackTrace}");
            }
        }
        if (attempts == 3) throw new Exception("Number of attempts to access to database was exceeded");
    }
}