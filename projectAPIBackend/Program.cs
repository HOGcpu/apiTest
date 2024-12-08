using ProjectApiBackend.Services;
using Serilog;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Loggiranje
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Log file location
    .CreateLogger();

builder.Logging.ClearProviders(); 
builder.Host.UseSerilog();        

// Konfiguracija iz appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Omogočimo vse naslove pošiljanja, 
// vse domene (origins), metode (GET, POST...) in glave (headers)
// Za vse domene ni varno, v realnih sistemih 
// uporaba žetona ali piškota za avtentikacijo
builder.Services.AddCors(options => 
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

// Dodajanja MessageServices in HttpClient
builder.Services.AddHttpClient<MessageService>();

// dodajanje kontrolerjev
builder.Services.AddControllers();

WebApplication? app = builder.Build();

app.UseCors("AllowFrontend");

// Dodajanje kontrolerjev
app.MapControllers();

app.Run();
