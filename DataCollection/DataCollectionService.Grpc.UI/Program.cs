using Common.EntityFramework;
using Common.SharedDomain;
using DataCollectionService.Application;
using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using DataCollectionService.Grpc.UI.Services;
using DataCollectionService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
if (!Directory.Exists(logFolderPath))
    Directory.CreateDirectory(logFolderPath);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        logFolderPath + "/log.txt",
        LogEventLevel.Information,
        outputTemplate: "`~{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddSerilog(Log.Logger);
builder.Services.AddGrpc();

builder.Services.AddSingleton<ICollector, VkCollector>();
builder.Services.AddSingleton<IVkApi, VkApi>(_ =>
{
    var vkSettings = builder.Configuration.GetSection("VkSettings");
    var vkApi = new VkApi();
    var authenticatedSuccessfully = vkApi.LogInAsync(new VkApiCredentials
    {
        ApplicationId = ulong.Parse(vkSettings["ApplicationId"]),
        SecureKey = vkSettings["SecureKey"],
        ServiceAccessKey = vkSettings["ServiceAccessKey"]
    }).GetAwaiter().GetResult();
    return authenticatedSuccessfully 
        ? vkApi 
        : throw new ApplicationException("Vk authorization failed! Stopping the application...");
});
builder.Services.AddSingleton<ICommentsRepository, CommentRepository>();
builder.Services.AddDbContextFactory<CommentsContext>(options =>
{
    var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
    if (!string.IsNullOrEmpty(postgresConnectionString))
    {
        options.UseNpgsql(postgresConnectionString);
    }
    else
    {
        var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServerConnectionString");
        options.UseSqlServer(sqlServerConnectionString);
    }
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.MapGrpcService<DataCollectionAPI>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
