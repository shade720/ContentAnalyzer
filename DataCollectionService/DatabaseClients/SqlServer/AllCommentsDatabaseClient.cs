using System.Data.SqlClient;
using Common;

namespace DataCollectionService.DatabaseClients.SqlServer;

public class AllCommentsDatabaseClient : MsSqlServerClient
{
    public AllCommentsDatabaseClient(string connectionString) : base(connectionString) { }
    public override void Add<T>(T commentData)
    {
        SafeAccess(() =>
        {
            var dataFrame = commentData as ICommentData;
            if (IsDataFrameInvalid(dataFrame)) return;
            var command = Connection.CreateCommand();
            command.CommandText = @"INSERT INTO [dbo].[AllComments] (CommentId, PostId, GroupId, AuthorId, Content, Date) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date)";
            command.Parameters.AddWithValue("@CommentId", dataFrame.Id);
            command.Parameters.AddWithValue("@PostId", dataFrame.PostId);
            command.Parameters.AddWithValue("@GroupId", dataFrame.GroupId);
            command.Parameters.AddWithValue("@AuthorId", dataFrame.AuthorId);
            command.Parameters.AddWithValue("@Content", dataFrame.Text);
            command.Parameters.AddWithValue("@Date", dataFrame.PostDate);
            command.ExecuteNonQuery();
        });
    }

    public override List<T> GetRange<T>(int startIndex)
    {
        var result = new List<ICommentData>();
        SafeAccess(() =>
        {
            using var command = new SqlCommand("SELECT Id, CommentId, PostId, GroupId, AuthorId, Content, Date FROM [dbo].[AllComments] WHERE Id > @StartIndex", Connection);
            command.Parameters.AddWithValue("@StartIndex", startIndex);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new CommentData(
                    Convert.ToInt64(reader["CommentId"]),
                    reader["Content"].ToString() ?? string.Empty,
                    Convert.ToInt64(reader["PostId"]),
                    Convert.ToInt64(reader["GroupId"]),
                    Convert.ToInt64(reader["AuthorId"]),
                    Convert.ToDateTime(reader["Date"])
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
            command.CommandText = "TRUNCATE TABLE [dbo].[AllComments]";
            command.ExecuteNonQuery();
        });
    }

    private static bool IsDataFrameInvalid(ICommentData dataFrame)
    {
        return string.IsNullOrEmpty(dataFrame.Text) || string.IsNullOrWhiteSpace(dataFrame.Text) || dataFrame.Text.Length < 5;
    }
}