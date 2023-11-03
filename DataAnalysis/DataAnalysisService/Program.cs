using Common.EntityFramework;
using DataAnalysisService.Services;
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
builder.WebHost.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddSerilog(Log.Logger);
});
builder.Services.AddGrpc();
builder.Services.AddDbContextFactory<CommentsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Configuration.SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.UserDomainName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddSingleton<DataAnalysisService.BusinessLogicLayer.DataAnalyzer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DataAnalysisAPI>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();