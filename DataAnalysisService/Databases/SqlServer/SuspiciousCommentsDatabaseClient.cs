using Common;
using System.Data.SqlClient;
using DataAnalysisService.AnalyzeModels.DomainClasses;

namespace DataAnalysisService.Databases.SqlServer;

public class SuspiciousCommentsDatabaseClient : MsSqlServerClient
{
    public SuspiciousCommentsDatabaseClient(string connectionString) : base(connectionString) { }
    public override void Add<T>(T result)
    {
        SafeAccess(()=>
        {
            if (result is not PredictResult commentData) return;
            var maxValue = commentData.Predicts.MaxBy(x => x.PredictValue);
            var command = Connection.CreateCommand();
            command.CommandText = @"INSERT INTO [dbo].[DangerCommentsContent] (CommentId, PostId, GroupId, AuthorId, Content, Date, Category, Probability) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date, @Category, @Probability)";
            command.Parameters.AddWithValue("@CommentId", commentData.CommentData.Id);
            command.Parameters.AddWithValue("@PostId", commentData.CommentData.PostId);
            command.Parameters.AddWithValue("@GroupId", commentData.CommentData.GroupId);
            command.Parameters.AddWithValue("@AuthorId", commentData.CommentData.AuthorId);
            command.Parameters.AddWithValue("@Content", commentData.CommentData.Text);
            command.Parameters.AddWithValue("@Date", commentData.CommentData.PostDate);
            command.Parameters.AddWithValue("@Category", maxValue.Title);
            command.Parameters.AddWithValue("@Probability", maxValue.PredictValue);
            command.ExecuteNonQuery();
        });
    }

    public override List<T> GetRange<T>(int startIndex)
    {
        var result = new List<IEvaluateResult>();
        SafeAccess(() =>
        {
            using var command = new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date, Category, Probability FROM [dbo].[DangerCommentsContent] WHERE Id > @StartIndex", Connection);
            command.Parameters.AddWithValue("@StartIndex", startIndex);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new EvaluateResult
                (
                    new CommentData(
                        Convert.ToInt64(reader["CommentId"]),
                        reader["Content"].ToString() ?? string.Empty,
                        Convert.ToInt64(reader["PostId"]),
                        Convert.ToInt64(reader["GroupId"]),
                        Convert.ToInt64(reader["AuthorId"]),
                        Convert.ToDateTime(reader["Date"])),
                    reader["Category"].ToString() ?? string.Empty,
                    Convert.ToDouble(reader["Probability"])
                ));
            }
        });
        return result as List<T> ?? new List<T>();
    }

    public override void Clear()
    {
        SafeAccess(() =>
        {
            var command = Connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE [dbo].[DangerCommentsContent]";
            command.ExecuteNonQuery();
        });
    }
}