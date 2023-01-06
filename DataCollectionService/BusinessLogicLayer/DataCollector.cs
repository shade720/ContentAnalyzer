using Common.EntityFramework;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DataCollectionService.BusinessLogicLayer.DatabaseClients;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient;

namespace DataCollectionService.BusinessLogicLayer;

public class DataCollector
{
    private readonly List<SocialNetworkClient> _socialNetworkClients;
    private readonly DatabaseClient<Comment> _saveDatabase;
    private readonly Stopwatch _workTimer;
    private readonly IConfiguration _configuration;
    public bool IsRunning => _workTimer.ElapsedTicks != 0;
    public string CurrentWorkingTime => _workTimer.Elapsed.ToString(@"hh\:mm\:ss");

    public DataCollector(IDbContextFactory<CommentsContext> contextFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _socialNetworkClients = GetConfiguredDataCollector(_configuration);
        _saveDatabase = new CommentsDatabaseClient(contextFactory);
        _workTimer = new Stopwatch();
    }

    public void StartCollecting()
    {
        foreach (var dataCollector in _socialNetworkClients)
        {
            dataCollector.StartCollecting();
            dataCollector.Subscribe(InsertToDatabase);
        }
        _workTimer.Start();
    }

    public void RestartCollecting()
    {
        StartCollecting();
        _socialNetworkClients.Clear();
        _socialNetworkClients.AddRange(GetConfiguredDataCollector(_configuration));
        StopCollecting();
    }

    public void StopCollecting()
    {
        foreach (var dataCollector in _socialNetworkClients)
        {
            dataCollector.StopCollecting();
            dataCollector.Unsubscribe(InsertToDatabase);
        }
        _workTimer.Stop();
        _workTimer.Reset();
    }

    public void ClearCollectedComments()
    {
        _saveDatabase.Clear();
    }

    public IEnumerable<Comment> GetCollectedComments(CommentsQueryFilter filter)
    {
        return _saveDatabase.GetRange(filter);
    }

    private void InsertToDatabase(Comment frame)
    {
        _saveDatabase.Add(frame);
    }

    private static List<SocialNetworkClient> GetConfiguredDataCollector(IConfiguration configuration)
    {
        var vkDataCollector = new VkClient();
        var vkSection = configuration.GetSection("VkSettings");
        vkDataCollector.Configure(configuration);
        foreach (var community in vkSection.GetSection("Communities").Get<List<string>>())
        {
            vkDataCollector.AddCommunity(Convert.ToInt64(community));
        }
        return new List<SocialNetworkClient> { vkDataCollector };
    }
}