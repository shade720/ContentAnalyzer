using Common;
using DataAnalysisService;
using Serilog;

var eventSink = new EventSink();
eventSink.OnLoggingEvent += log => Logger.Log(log);

Log.Logger = new LoggerConfiguration()
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

Startup.ConfigureService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DataAnalysisService.Services.DataAnalysisService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();