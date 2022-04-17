using VkDataCollector;
using System.Configuration;
using Common;
using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataCollectionService;

public static class Startup
{
    public static void ConfigureService()
    {
        DataCollectionService.SetDatabaseContextOption(new DbContextOptionsBuilder<CommentsContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;")
            .Options);

        DataCollectionService.AddDataCollector(() =>
        {
            var vkDataCollector = new VkDataCollector.VkDataCollector();
            vkDataCollector.Configure(new Config
            {
                ApplicationId = Convert.ToUInt64(ConfigurationManager.AppSettings["VkApplicationId"]),
                SecureKey = ConfigurationManager.AppSettings["VkSecureKey"],
                ServiceAccessKey = ConfigurationManager.AppSettings["VkServiceAccessKey"],
                ScanCommentsDelay = Convert.ToInt32(ConfigurationManager.AppSettings["ScanCommentsDelay"]),
                ScanPostDelay = Convert.ToInt32(ConfigurationManager.AppSettings["ScanPostDelay"]),
                QueueSize = Convert.ToInt32(ConfigurationManager.AppSettings["PostQueueSize"])
            });
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["NRGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["LentachGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["CSGOHSGroupId"]));
            vkDataCollector.Subscribe(commentData => Log.Logger.Information("Add {commentData.CommentId} {commentData.PostId} {commentData.GroupId} {commentData.AuthorId} {commentData.Text} {commentData.PostDate}", commentData.CommentId, commentData.PostId, commentData.GroupId, commentData.AuthorId, commentData.Text, commentData.PostDate));
            return vkDataCollector;
        });
    }
}
