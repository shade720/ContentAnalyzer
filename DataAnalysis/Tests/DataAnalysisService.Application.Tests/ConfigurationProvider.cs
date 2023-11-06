using Microsoft.Extensions.Configuration;

namespace DataAnalysisService.Application.Tests;

public class ConfigurationProvider
{
    public static IConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();
        return config;
    }
}