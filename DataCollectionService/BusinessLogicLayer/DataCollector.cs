using Common.EntityFramework;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DataCollectionService.BusinessLogicLayer.DatabaseClients;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Dynamic;
using Serilog;

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

    public bool UpdateSettings(string settings)
    {
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var appSettingsJson = File.ReadAllText(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? oldConfig = JsonConvert.DeserializeObject<ExpandoObject>(appSettingsJson, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(settings, jsonSettings);

        if (oldConfig is null)
        {
            Log.Logger.Error("Cannot deserialize appsettings.json file");
            return false;
        }
        if (newConfig is null)
        {
            Log.Logger.Error("Cannot deserialize new settings file");
            return false;
        }

        var newConfigDict = (IDictionary<string, object>)newConfig;
        var oldConfigDict = (IDictionary<string, object>)oldConfig;

        foreach (var pair in newConfigDict)
        {
            if (oldConfigDict.ContainsKey(pair.Key))
                oldConfigDict[pair.Key] = pair.Value;
        }

        var newAppSettingsJson = JsonConvert.SerializeObject(oldConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(appSettingsPath, newAppSettingsJson);
        return true;
    }
}