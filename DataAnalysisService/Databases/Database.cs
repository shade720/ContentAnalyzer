using System.Data.SqlClient;
using Interfaces;

namespace DataAnalysisService.Databases
{
    public class Database
    {
        public Database(string connectionString) => _connection = new SqlConnection(connectionString);
        private readonly SqlConnection _connection;

        private SqlDependency? _dependency;

        private readonly List<long> _receivedIDs = new();

        private delegate void OnDataReceived(IDataFrame data);
        private event OnDataReceived OnDataReceivedEvent;

        public void Connect()
        {
            _connection.Open();
            _dependency = new SqlDependency(new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date FROM [dbo].[Table]", _connection));
            _dependency.OnChange += OnDependencyChange;
            SqlDependency.Start(_connection.ConnectionString);
        }

        public void Disconnect()
        {
            SqlDependency.Stop(_connection.ConnectionString);
            _dependency.OnChange -= OnDependencyChange;
            _connection.Close();
        }

        public void StartLoading()
        {
            using var command = new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date FROM [dbo].[Table]", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var receivedId = Convert.ToInt64(reader["Id"]);
                if (_receivedIDs.Contains(receivedId)) continue;
                _receivedIDs.Add(receivedId);
                OnDataReceivedEvent.Invoke(new DataFrame(
                    Convert.ToInt64(reader["CommentId"]),
                    reader["Content"].ToString(),
                    Convert.ToInt64(reader["PostId"]),
                    Convert.ToInt64(reader["GroupId"]),
                    Convert.ToInt64(reader["AuthorId"]),
                    Convert.ToDateTime(reader["Date"])
                ));
            }
        }

        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            StartLoading();
        }

        public void SetIncomingDataHandler(Action<IDataFrame> handler)
        {
            OnDataReceivedEvent += handler.Invoke;
        }

        public void Clear()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE [Table]";
            command.ExecuteNonQuery();
        }
    }
}
