using System.Data.SqlClient;
using Interfaces;

namespace DataCollectionService.Databases.SqlServer;

public class AllCommentsDatabaseClient
{
    public AllCommentsDatabaseClient(string connectionString) => _connection = new SqlConnection(connectionString);

    private readonly SqlConnection _connection;

    public void Connect() => _connection.Open();
    public void Disconnect() => _connection.Close();

    public void Add(IDataFrame dataFrame)
    {
        if (IsDataFrameInvalid(dataFrame)) return;
        var command = _connection.CreateCommand();
        command.CommandText = @"INSERT INTO [dbo].[AllComments] (CommentId, PostId, GroupId, AuthorId, Content, Date) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date)";
        command.Parameters.AddWithValue("@CommentId", dataFrame.Id);
        command.Parameters.AddWithValue("@PostId", dataFrame.PostId);
        command.Parameters.AddWithValue("@GroupId", dataFrame.GroupId);
        command.Parameters.AddWithValue("@AuthorId", dataFrame.AuthorId);
        command.Parameters.AddWithValue("@Content", dataFrame.Text);
        command.Parameters.AddWithValue("@Date", dataFrame.PostDate);
        command.ExecuteNonQuery();
    }

    public void Clear()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "TRUNCATE TABLE [dbo].[AllComments]";
        command.ExecuteNonQuery();
    }

    private static bool IsDataFrameInvalid(IDataFrame dataFrame)
    {
        var result = string.IsNullOrEmpty(dataFrame.Text) || string.IsNullOrWhiteSpace(dataFrame.Text) || dataFrame.Text.Length < 5;
        return result;
    }
}