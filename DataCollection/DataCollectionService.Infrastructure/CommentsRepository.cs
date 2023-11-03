﻿using Common.EntityFramework;
using DataCollectionService.Application;
using DataCollectionService.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CommentsQueryFilter = DataCollectionService.Domain.CommentsQueryFilter;

namespace DataCollectionService.Infrastructure;

public class CommentRepository : ICommentsRepository
{
    private readonly IDbContextFactory<CommentsContext> _contextFactory;

    public CommentRepository(IDbContextFactory<CommentsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task Add(VkComment comment)
    {
        if (IsCommentInvalid(comment)) 
            return;
        await using var context = await _contextFactory.CreateDbContextAsync();

        if (context.Comments.Any(x => x.CommentId == comment.CommentId))
            return;
        context.Comments.Add(CommentsConverter.ConvertFromVkComment(comment));
        await context.SaveChangesAsync();
        Log.Logger.Information("Add {0} {1} {2} {3} {4} {5}",
            comment.CommentId,
            comment.PostId,
            comment.GroupId,
            comment.AuthorId,
            comment.Text,
            comment.PostDate);
        Log.Logger.Information("{0} comments collected", context.Comments.Count());
    }

    public async Task<IQueryable<VkComment>> GetRange(CommentsQueryFilter filter)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return context.Comments
            .Where(c => c.Id > filter.Id)
            .Select(c => CommentsConverter.ConvertToVkComment(c));
    }

    public async Task Clear()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        await context.Comments.ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }

    private static bool IsCommentInvalid(VkComment comment)
    {
        return 
            string.IsNullOrEmpty(comment.Text) || 
            string.IsNullOrWhiteSpace(comment.Text) || 
            comment.Text.Length < 5;
    }
}