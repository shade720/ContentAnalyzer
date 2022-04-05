using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAnalysisService.DatabaseClients.SqlServer;

public class AllCommentsDatabaseObserver : IDatabaseObserver
{
    private readonly SqlConnection _connection;
    private long _lastReceivedId;
    private readonly int _observeDelay;
    private CancellationTokenSource _cancellation;
    private Action<ICommentData> _dataProcessor;

    public AllCommentsDatabaseObserver(string connectionString, int observeDelayMs) => (_connection, _observeDelay) = (new SqlConnection(connectionString), observeDelayMs);

    #region PublicInterface

    public bool IsLoadingStarted { get; private set; }

    public void StartLoading()
    {
        if (IsLoadingStarted) throw new Exception("Loading already started");
        if (_connection.State == ConnectionState.Open) throw new Exception("Loading not started but connection was open");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");

        IsLoadingStarted = true;
        _connection.Open();
        _cancellation = new CancellationTokenSource();
        var result = LoadingLoop(_cancellation.Token);
        Logger.Log($"Loading started on {_connection.ConnectionString} with delay {_observeDelay}", Logger.LogLevel.Information);
    }

    public void StopLoading()
    {
        if(!IsLoadingStarted) throw new Exception($"Loading already stopped {nameof(StopLoading)}");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");
        if (_connection.State == ConnectionState.Closed) throw new Exception("Loading not stopped but connection was closed");

        IsLoadingStarted = false;
        _cancellation.Cancel();
        _connection.Close();
        Logger.Log($"Loading stopped on {_connection.ConnectionString}", Logger.LogLevel.Information);
    }

    public void OnDataArrived(Action<ICommentData> handler) => _dataProcessor = handler;

    #endregion

    private async Task LoadingLoop(CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                LoadData();
                await Task.Delay(_observeDelay, cancellationToken);
            }
        }, cancellationToken);
    }

    private void LoadData()
    {
        SafeAccess(() =>
        {
            using var command = new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date FROM [dbo].[AllComments] WHERE Id > @StartIndex", _connection);
            command.Parameters.AddWithValue("@StartIndex", _lastReceivedId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _lastReceivedId = Convert.ToInt64(reader["Id"]);
                _dataProcessor?.Invoke(new CommentData
                (
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
    private static void SafeAccess(Action accessAction)
    {
        int attempts;
        const int retryDelayMs = 5000;
        for (attempts = 0; attempts < 3; attempts++)
        {
            try
            {
                accessAction.Invoke();
                break;
            }
            catch (Exception e)
            {
                Logger.Log($"{e.Message} {e.StackTrace}", Logger.LogLevel.Fatal);
                Thread.Sleep(retryDelayMs);
            }
        }
        if (attempts == 3) throw new Exception("Number of attempts to access to database was exceeded");
    }
}