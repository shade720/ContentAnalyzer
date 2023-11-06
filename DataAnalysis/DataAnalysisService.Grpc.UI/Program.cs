using Common.EntityFramework;
using Common.SharedDomain;
using DataAnalysisService.Application;
using DataAnalysisService.Domain.Abstractions;
using DataAnalysisService.Grpc.UI.Services;
using DataAnalysisService.Infrastructure;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapGrpcService<DataAnalysisAPI>();

app.Run();