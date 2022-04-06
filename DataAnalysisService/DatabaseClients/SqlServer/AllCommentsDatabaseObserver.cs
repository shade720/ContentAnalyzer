using System.Data.SqlClient;
using Common;

namespace DataAnalysisService.DatabaseClients.SqlServer;

public class AllCommentsDatabaseObserver : MsSqlServerObserver
{
    private long _lastReceivedId;
    private CancellationTokenSource _cancellation;
    private Action<ICommentData> _dataProcessor;
    private readonly int _observeDelay;

    public AllCommentsDatabaseObserver(string connectionString, int observeDelayMs) : base(connectionString) => 
        _observeDelay = observeDelayMs;
    

    #region PublicInterface

    public override void StartLoading()
    {
        if (IsLoadingStarted) throw new Exception("Loading already started");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");

        Connect();
        _cancellation = new CancellationTokenSource();
        IsLoadingStarted = true;
        var result = LoadingLoop(_cancellation.Token);
        Logger.Log($"Loading started on with delay {_observeDelay}", Logger.LogLevel.Information);
    }

    public override void StopLoading()
    {
        if(!IsLoadingStarted) throw new Exception($"Loading already stopped {nameof(StopLoading)}");
        if (_dataProcessor is null) throw new Exception($"Data processor not set {nameof(StopLoading)}");

        _cancellation.Cancel();
        IsLoadingStarted = false;
        Disconnect();
        Logger.Log("Loading stopped", Logger.LogLevel.Information);
    }

    public override void OnDataArrived(Action<ICommentData> handler) => _dataProcessor = handler;

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
            using var command = new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date FROM [dbo].[AllComments] WHERE Id > @StartIndex", Connection);
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
}