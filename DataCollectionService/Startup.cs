using System.Collections;
using System.Configuration;
using Common;
using Common.EntityFramework;
using DataCollectionService.DataCollectors.VkDataCollector;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataCollectionService;

public static class Startup
{

    public static void ConfigureService(IConfiguration configuration)
    {
        Services.DataCollectionService.SetDatabaseContextOption(new DbContextOptionsBuilder<CommentsContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;")
            .Options);

        Services.DataCollectionService.AddDataCollector(() =>
        {
            var vkDataCollector = new VkDataCollector();
            vkDataCollector.Configure(new Config
            {
                ApplicationId = Convert.ToUInt64(configuration["VkApplicationId"]),
                SecureKey = configuration["VkSecureKey"],
                ServiceAccessKey = configuration["VkServiceAccessKey"],
                ScanCommentsDelay = Convert.ToInt32(configuration["ScanCommentsDelay"]),
                ScanPostDelay = Convert.ToInt32(configuration["ScanPostDelay"]),
                QueueSize = Convert.ToInt32(configuration["PostQueueSize"])
            });
            vkDataCollector.AddCommunity(Convert.ToInt64(configuration["NRGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(configuration["LentachGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(configuration["CSGOHSGroupId"]));
            vkDataCollector.Subscribe(commentData => Log.Logger.Information("Add {commentData.CommentId} {commentData.PostId} {commentData.GroupId} {commentData.AuthorId} {commentData.Text} {commentData.PostDate}", commentData.CommentId, commentData.PostId, commentData.GroupId, commentData.AuthorId, commentData.Text, commentData.PostDate));
            return vkDataCollector;
        });
    }
}
