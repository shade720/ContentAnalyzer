using System.Data;
using System.Data.SqlClient;
using Interfaces;

namespace DataAnalysisService.Databases.SqlServer;

public class AllCommentsDatabaseObserver
{
    private readonly SqlConnection _connection;
    private readonly List<long> _receivedIDs = new();
    private readonly int _observeDelay;
    private CancellationTokenSource _cancellation;

    private delegate void OnDataReceived(IDataFrame data);
    private event OnDataReceived OnDataReceivedEvent;

    public AllCommentsDatabaseObserver(string connectionString, int observeDelayMs) => 
        (_connection, _observeDelay) = (new SqlConnection(connectionString), observeDelayMs);

    #region PublicInterface

    public void StartLoading()
    {
        _connection.Open();
        _cancellation = new CancellationTokenSource();
        var result = ObserveLoop(_cancellation.Token);
    }

    public void StopLoading()
    {
        _cancellation.Cancel();
        _connection.Close();
    }

    public void SetIncomingDataHandler(Action<IDataFrame> handler)
    {
        OnDataReceivedEvent += handler.Invoke;
    }

    #endregion

    private async Task ObserveLoop(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                LoadData();
                Task.Delay(_observeDelay, cancellationToken);
            }
        }, cancellationToken);
    }

    private void LoadData()
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
    }
}