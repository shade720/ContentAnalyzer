﻿using System.Data;
using System.Data.SqlClient;
using Interfaces;

namespace DataCollectionService.Databases.SqlServer
{
    public class Database
    {
        public Database(string connectionString) => _connection = new SqlConnection(connectionString);

        private readonly SqlConnection _connection;

        public void Connect()
        {
            _connection.Open();
        }

        public void Disconnect()
        {
            _connection.Close();
        }

        public void Add(IDataFrame dataFrame)
        {
            if (IsDataFrameInvalid(dataFrame)) return;
            var command = _connection.CreateCommand();
            command.CommandText = @"INSERT INTO [dbo].[Table] (CommentId, PostId, GroupId, AuthorId, Content, Date) VALUES (@CommentId, @PostId, @GroupId, @AuthorId, @Content, @Date)";
            command.Parameters.Add("@CommentId", SqlDbType.BigInt).Value = dataFrame.Id;
            command.Parameters.Add("@PostId", SqlDbType.BigInt).Value = dataFrame.PostId;
            command.Parameters.Add("@GroupId", SqlDbType.BigInt).Value = dataFrame.GroupId;
            command.Parameters.Add("@AuthorId", SqlDbType.BigInt).Value = dataFrame.AuthorId;
            command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = dataFrame.Text;
            command.Parameters.Add("@Date", SqlDbType.DateTime).Value = dataFrame.PostDate;
            command.ExecuteNonQuery();
        }

        public void Clear()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE [Table]";
            command.ExecuteNonQuery();
        }

        private static bool IsDataFrameInvalid(IDataFrame dataFrame)
        {
            var result = string.IsNullOrEmpty(dataFrame.Text) || string.IsNullOrWhiteSpace(dataFrame.Text) ||
                         dataFrame.Text.Length < 5;
            return result;
        }
    }
}