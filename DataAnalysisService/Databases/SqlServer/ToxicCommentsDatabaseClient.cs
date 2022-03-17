using System.Data.SqlClient;
using System.Globalization;
using Interfaces;
using M_USE_Toxic;

namespace DataAnalysisService.Databases.SqlServer;

public class ToxicCommentsDatabaseClient
{
    public ToxicCommentsDatabaseClient(string connectionString) => _connection = new SqlConnection(connectionString);

    private readonly SqlConnection _connection;

    public void Connect() => _connection.Open();
    public void Disconnect() => _connection.Close();

    public void Add(PredictResult result)
    {
        var command = _connection.CreateCommand();
        command.CommandText = @"INSERT INTO [dbo].[ToxicComments] (CommentId, PostId, GroupId, AuthorId, Content, Date, Toxicity) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date, @Toxicity)";
        command.Parameters.AddWithValue("@CommentId", result.DataFrame.Id);
        command.Parameters.AddWithValue("@PostId", result.DataFrame.PostId);
        command.Parameters.AddWithValue("@GroupId", result.DataFrame.GroupId);
        command.Parameters.AddWithValue("@AuthorId", result.DataFrame.AuthorId);
        command.Parameters.AddWithValue("@Content", result.DataFrame.Text);
        command.Parameters.AddWithValue("@Date", result.DataFrame.PostDate);
        command.Parameters.AddWithValue("@Toxicity", double.Parse(result.Toxicity, CultureInfo.InvariantCulture));
        command.ExecuteNonQuery();
    }

    public void Clear()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "TRUNCATE TABLE [dbo].[ToxicComments]";
        command.ExecuteNonQuery();
    }
}