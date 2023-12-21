using Common.EntityFramework;
using Common.SharedDomain;
using DataAnalysisService.Application;
using DataAnalysisService.Domain.Abstractions;
using DataAnalysisService.Grpc.UI.Services;
using DataAnalysisService.Infrastructure;
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
builder.Configuration.SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.UserDomainName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddGrpc();

builder.Services.AddSingleton<DataAnalyzer>();
builder.Services.AddSingleton<IArtificialIntelligenceModelFactory, BertModelFactory>();
builder.Services.AddSingleton<ICommentsObserver, CommentsDatabaseObserver>();
builder.Services.AddSingleton<ICommentsRepository, CommentsRepository>();
builder.Services.AddSingleton<IEvaluatedCommentsRepository, EvaluatedCommentsRepository>();


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
app.MapGrpcService<DataAnalysisAPI>();

app.Run();