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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.RollingFile(
        @".\Logs\log{Date}.txt",
        LogEventLevel.Information,
        outputTemplate: "`~{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        retainedFileCountLimit: 3)
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

app.MapGrpcService<DataCollectionAPI>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
