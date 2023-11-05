using Microsoft.Extensions.Configuration;

namespace DataCollectionService.Infrastructure.Tests;

internal class ConfigurationProvider
{
    public static IConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings_test.json")
            .Build();
        return config;
    }
}