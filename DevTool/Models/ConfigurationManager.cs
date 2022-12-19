using Newtonsoft.Json;

namespace DevTool.Models;

internal static class ConfigurationManager
{
    public static Configuration? GetConfiguration()
    {
        if (!File.Exists(@"configuration.json")) return null;
        var configFile = File.ReadAllText(@"configuration.json");
        return JsonConvert.DeserializeObject<Configuration>(configFile);
    }
    public static void SaveConfiguration(Configuration configuration)
    {
        File.WriteAllText(@"configuration.json", JsonConvert.SerializeObject(configuration));
    }

    public static VkConfiguration? GetVkConfiguration()
    {
        if (!File.Exists(@"vkSettings.json")) return null;
        var configFile = File.ReadAllText(@"vkSettings.json");
        return JsonConvert.DeserializeObject<VkConfiguration>(configFile);
    }
    public static void SaveVkConfiguration(VkConfiguration configuration)
    {
        File.WriteAllText(@"vkSettings.json", JsonConvert.SerializeObject(configuration));
    }
}