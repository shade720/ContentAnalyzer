using Interfaces;
using System.Data.SqlClient;

namespace DataAnalysisService
{
    public class MSSQLDB
    {
        private readonly SqlConnection _connection = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Projects\Prototype\ContentAnalyzer\DataCollectionService\Databases\MSSQLDB\MSSQLDB.mdf;Integrated Security=SSPI");
        public delegate void OnDataReceived(IDataFrame data);
        public event OnDataReceived OnDataReceivedEvent;
        private SqlDependency _dependency;
        private readonly List<long> _receivedIDs = new();

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

        public void LoadData()
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
            LoadData();
        }


        public void Clear()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE [Table]";
            command.ExecuteNonQuery();
        }
    }
}
