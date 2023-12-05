using ContentAnalyzer.Gateway.EFCoreIdentity;
using ContentAnalyzer.Gateway.GrpcClients;
using ContentAnalyzer.Gateway.Interceptors;
using ContentAnalyzer.Gateway.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        @".\Logs\log{Date}.txt",
        LogEventLevel.Information,
        outputTemplate: "`~{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        retainedFileCountLimit: 3)
    .CreateLogger();

using var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(Log.Logger));
var logger = loggerFactory.CreateLogger<Program>();

builder.Logging.ClearProviders();
builder.Services.AddSingleton<ILogger>(logger);

builder.Services.AddDbContextFactory<IdentificationContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
    .AddIdentity<Token, IdentityRole>()
    .AddEntityFrameworkStores<IdentificationContext>();
builder.Services.AddScoped<TokenManager>();
builder.Services.AddScoped<DataCollectionServiceClient>();
builder.Services.AddScoped<DataAnalysisServiceClient>();

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<AuthenticationInterceptor>();
});

var app = builder.Build();

app.UseAuthentication();
//app.UseAuthorization();

app.MapGrpcService<ContentAnalyzerGateway>();

app.Run();
