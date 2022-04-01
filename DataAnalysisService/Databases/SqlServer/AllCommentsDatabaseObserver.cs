using System.Data.SqlClient;
using Common;

namespace DataAnalysisService.Databases.SqlServer;

public class AllCommentsDatabaseObserver : IDatabaseObserver
{
    private readonly SqlConnection _connection;
    private readonly List<long> _receivedIDs = new();
    private readonly int _observeDelay;
    private CancellationTokenSource _cancellation;
    private Action<ICommentData> _newDataHandler;

    private delegate void OnDataReceived(ICommentData data);
    private event OnDataReceived OnDataReceivedEvent;

    public AllCommentsDatabaseObserver(string connectionString, int observeDelayMs) => (_connection, _observeDelay) = (new SqlConnection(connectionString), observeDelayMs);

    #region PublicInterface

    public bool IsLoadingStarted { get; private set; }

    public void StartLoading()
    {
        if(IsLoadingStarted) throw new Exception("Loading already started");
        IsLoadingStarted = true;
        OnDataReceivedEvent += _newDataHandler.Invoke;
        _connection.Open();
        _cancellation = new CancellationTokenSource();
        var result = LoadingLoop(_cancellation.Token);
        Logger.Write($"Loading started on {_connection.ConnectionString} with delay {_observeDelay}");
    }

    public void StopLoading()
    {
        if(!IsLoadingStarted) throw new Exception($"Loading already stopped {nameof(StopLoading)}");
        if (_newDataHandler is null) throw new Exception($"Handler not set {nameof(StopLoading)}");
        IsLoadingStarted = false;
        OnDataReceivedEvent -= _newDataHandler.Invoke;
        _cancellation.Cancel();
        _connection.Close();
        Logger.Write($"Loading stopped on {_connection.ConnectionString}");
    }

    public void OnDataArrived(Action<ICommentData> handler) => _newDataHandler = handler;

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
                OnDataReceivedEvent.Invoke(new CommentData(
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
                Logger.Write($"{e.Message} {e.StackTrace}");
            }
        }
        if (attempts == 3) throw new Exception("Number of attempts to access to database was exceeded");
    }
}