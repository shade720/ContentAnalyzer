using System.Data.SqlClient;
using DataAnalysisService.AnalyzeModelController;

namespace DataAnalysisService.Databases.SqlServer;

public class DangerCommentsDatabaseClient
{
    public DangerCommentsDatabaseClient(string connectionString) => _connection = new SqlConnection(connectionString);

    private readonly SqlConnection _connection;

    public void Connect() => _connection.Open();
    public void Disconnect() => _connection.Close();

    public void Add(PredictResult result)
    {
        var command1 = _connection.CreateCommand();
        command1.CommandText = @"INSERT INTO [dbo].[DangerCommentsContent] (CommentId, PostId, GroupId, AuthorId, Content, Date) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date)";
        command1.Parameters.AddWithValue("@CommentId", result.DataFrame.Id);
        command1.Parameters.AddWithValue("@PostId", result.DataFrame.PostId);
        command1.Parameters.AddWithValue("@GroupId", result.DataFrame.GroupId);
        command1.Parameters.AddWithValue("@AuthorId", result.DataFrame.AuthorId);
        command1.Parameters.AddWithValue("@Content", result.DataFrame.Text);
        command1.Parameters.AddWithValue("@Date", result.DataFrame.PostDate);
        command1.ExecuteNonQuery();

        var command2 = _connection.CreateCommand();
        command2.CommandText = @"INSERT INTO [dbo].[InsultCategories] (CommentId, Normal, Insult, Threat, Obscenity) VALUES (@CommentId, @Normal, @Insult, @Threat, @Obscenity)";
        command2.Parameters.AddWithValue("@CommentId", result.DataFrame.Id);
        command2.Parameters.AddWithValue("@Normal", result.Predicts[0].PredictValue);
        command2.Parameters.AddWithValue("@Insult", result.Predicts[1].PredictValue);
        command2.Parameters.AddWithValue("@Threat", result.Predicts[2].PredictValue);
        command2.Parameters.AddWithValue("@Obscenity", result.Predicts[3].PredictValue);
        command2.ExecuteNonQuery(); 
    }

    public void Clear()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "TRUNCATE TABLE [dbo].[DangerCommentsContent]";
        command.ExecuteNonQuery();
    }
}