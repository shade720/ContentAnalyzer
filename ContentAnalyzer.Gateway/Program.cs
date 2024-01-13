using ContentAnalyzer.Gateway.EFCoreIdentity;
using ContentAnalyzer.Gateway.GrpcClients;
using ContentAnalyzer.Gateway.Interceptors;
using ContentAnalyzer.Gateway.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
if (!Directory.Exists(logFolderPath))
    Directory.CreateDirectory(logFolderPath);

var builder = WebApplication.CreateBuilder(args);

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

using var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(Log.Logger));
var logger = loggerFactory.CreateLogger<Program>();

builder.Logging.ClearProviders();
builder.Services.AddSingleton<ILogger>(logger);

builder.Services.AddDbContextFactory<IdentificationContext>(options =>
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
builder.Services
    .AddIdentity<Token, IdentityRole>()
    .AddEntityFrameworkStores<IdentificationContext>();
builder.Services.AddScoped<TokenManager>();
builder.Services.AddScoped<DataCollectionServiceClient>();
builder.Services.AddScoped<DataAnalysisServiceClient>();

builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 1024 * 1024 * 1024;
    options.MaxSendMessageSize = 1024 * 1024 * 1024;
    options.Interceptors.Add<AuthenticationInterceptor>();
});

var app = builder.Build();

app.UseAuthentication();
//app.UseAuthorization();

app.MapGrpcService<ContentAnalyzerGateway>();

app.Run();
