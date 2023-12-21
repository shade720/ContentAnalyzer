using ContentAnalyzer.Frontend.Desktop.Models;
using Newtonsoft.Json;

namespace ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;

internal static class ConfigurationManager
{
    public static AppConfiguration? GetAppConfiguration()
    {
        if (!File.Exists(@"configuration.json")) return null;
        var configFile = File.ReadAllText(@"configuration.json");
        return JsonConvert.DeserializeObject<AppConfiguration>(configFile);
    }

    public static void SaveAppConfiguration(AppConfiguration configuration)
    {
        File.WriteAllText(@"configuration.json", JsonConvert.SerializeObject(configuration));
    }

    public static CollectionServiceConfiguration? GetCollectionServiceConfiguration()
    {
        if (!File.Exists(@"collection-service-configuration.json")) 
            return null;
        var configFile = File.ReadAllText(@"collection-service-configuration.json");
        return JsonConvert.DeserializeObject<CollectionServiceConfiguration>(configFile);
    }

    public static void SaveCollectionConfiguration(CollectionServiceConfiguration configuration)
    {
        File.WriteAllText(@"collection-service-configuration.json", JsonConvert.SerializeObject(configuration));
    }

    public static AnalysisServiceConfiguration? GetAnalysisServiceConfiguration()
    {
        if (!File.Exists(@"analysis-service-configuration.json"))
            return null;
        var configFile = File.ReadAllText(@"analysis-service-configuration.json");
        return JsonConvert.DeserializeObject<AnalysisServiceConfiguration>(configFile);
    }

    public static void SaveAnalysisConfiguration(AnalysisServiceConfiguration configuration)
    {
        File.WriteAllText(@"analysis-service-configuration.json", JsonConvert.SerializeObject(configuration));
    }
}