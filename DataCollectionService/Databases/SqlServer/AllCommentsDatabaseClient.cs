using Interfaces;

namespace DataCollectionService.Databases.SqlServer;

public class AllCommentsDatabaseClient : MsSqlServerClient
{
    public AllCommentsDatabaseClient(string connectionString) : base(connectionString) { }
    public override void Add<T>(T dataFrame)
    {
        SafeAccess(() =>
        {
            var dataFrameCast = dataFrame as ICommentData;
            if (IsDataFrameInvalid(dataFrameCast)) return;
            var command = Connection.CreateCommand();
            command.CommandText = @"INSERT INTO [dbo].[AllComments] (CommentId, PostId, GroupId, AuthorId, Content, Date) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date)";
            command.Parameters.AddWithValue("@CommentId", dataFrameCast.Id);
            command.Parameters.AddWithValue("@PostId", dataFrameCast.PostId);
            command.Parameters.AddWithValue("@GroupId", dataFrameCast.GroupId);
            command.Parameters.AddWithValue("@AuthorId", dataFrameCast.AuthorId);
            command.Parameters.AddWithValue("@Content", dataFrameCast.Text);
            command.Parameters.AddWithValue("@Date", dataFrameCast.PostDate);
            command.ExecuteNonQuery();
        });
    }

    public override void Clear()
    {
        SafeAccess(() =>
        {
            var command = Connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE [dbo].[AllComments]";
            command.ExecuteNonQuery();
        });
    }

    private static bool IsDataFrameInvalid(ICommentData dataFrame)
    {
        var result = string.IsNullOrEmpty(dataFrame.Text) || string.IsNullOrWhiteSpace(dataFrame.Text) || dataFrame.Text.Length < 5;
        return result;
    }
}