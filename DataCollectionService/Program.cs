using Common;
using Common.EntityFramework;
using DataCollectionService;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var eventSink = new EventSink();
eventSink.OnLoggingEvent += Logger.Log;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.Sink(eventSink)
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
Startup.ConfigureService(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<DataCollectionService.Services.DataCollectionService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
