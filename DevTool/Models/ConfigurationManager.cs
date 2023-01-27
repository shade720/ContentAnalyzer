using System.Reflection;
using Newtonsoft.Json;

namespace DevTool.Models;

internal static class ConfigurationManager
{
    private const string ConfigurationDirectoryName = "configurations";
    private const string ConfigurationFile = "configuration.json";
    private const string DefaultProfile = "default";

    static ConfigurationManager()
    {
        if (!Directory.Exists(ConfigurationDirectoryName)) 
            Directory.CreateDirectory(ConfigurationDirectoryName);
    }

    private static string GetProfileConfigurationFilename(string profileName, MemberInfo configurationType) => 
        profileName.Replace(":", "-").Replace("/","") + "-" + configurationType.Name + "-" + ConfigurationFile;

    public static T GetConfiguration<T>(string profileName = DefaultProfile) where T : new()
    {
        var configPath = Path.Combine(ConfigurationDirectoryName, GetProfileConfigurationFilename(profileName, typeof(T)));
        if (!File.Exists(configPath)) File.Create(configPath).Close();
        var configFile = File.ReadAllText(configPath);
        var configuration = JsonConvert.DeserializeObject<T>(configFile);
        return configuration ?? new T();
    }

    public static string GetConfigurationContent<T>(string profileName = DefaultProfile) where T : new()
    {
        var configPath = Path.Combine(ConfigurationDirectoryName, GetProfileConfigurationFilename(profileName, typeof(T)));
        if (!File.Exists(configPath)) File.Create(configPath).Close();
        var configFile = File.ReadAllText(configPath);
        return configFile;
    }

    public static void SaveConfiguration<T>(T configuration, string profileName = DefaultProfile)
    {
        var configPath = Path.Combine(ConfigurationDirectoryName, GetProfileConfigurationFilename(profileName, typeof(T)));
        File.WriteAllText(configPath, JsonConvert.SerializeObject(configuration));
    }
}