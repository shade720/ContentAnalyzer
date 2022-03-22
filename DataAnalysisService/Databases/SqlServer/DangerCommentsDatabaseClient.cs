using DataAnalysisService.AnalyzeModelController;
using Interfaces;

namespace DataAnalysisService.Databases.SqlServer;

public class DangerCommentsDatabaseServerClient : MsSqlServerClient
{
    public DangerCommentsDatabaseServerClient(string connectionString) : base(connectionString) { }
    public override void Add<T>(T result)
    {
        SafeAccess(()=>{
            var predict = result as PredictResult;
            var maxValue = predict.Predicts.MaxBy(x => x.PredictValue);
            var command = Connection.CreateCommand();
            command.CommandText = @"INSERT INTO [dbo].[DangerCommentsContent] (CommentId, PostId, GroupId, AuthorId, Content, Date, Category, Probability) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date, @Category, @Probability)";
            command.Parameters.AddWithValue("@CommentId", predict.DataFrame.Id);
            command.Parameters.AddWithValue("@PostId", predict.DataFrame.PostId);
            command.Parameters.AddWithValue("@GroupId", predict.DataFrame.GroupId);
            command.Parameters.AddWithValue("@AuthorId", predict.DataFrame.AuthorId);
            command.Parameters.AddWithValue("@Content", predict.DataFrame.Text);
            command.Parameters.AddWithValue("@Date", predict.DataFrame.PostDate);
            command.Parameters.AddWithValue("@Category", maxValue.Title);
            command.Parameters.AddWithValue("@Probability", maxValue.PredictValue);
            command.ExecuteNonQuery();
        });
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