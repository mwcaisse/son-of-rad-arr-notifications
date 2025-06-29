
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using SonOfRadArrNotifications.Configuration;
using SonOfRadArrNotifications.Radarr;
using SonOfRadArrNotifications.Services;
using SonOfRadArrNotifications.Sonarr;
using SonOfRadArrNotifications.Tasks;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up application");



var builder = WebApplication.CreateBuilder(args);

// Handle our configuration
builder.Configuration.AddJsonFile("application.settings.json", optional: true)
    .AddEnvironmentVariables("SORAN_");

var emailConfiguration = new EmailConfiguration()
{
    Region = builder.Configuration.GetValue<string>("EMAIL_REGION")!,
    AccessKey = builder.Configuration.GetValue<string>("EMAIL_ACCESS_KEY")!,
    SecretKey = builder.Configuration.GetValue<string>("EMAIL_SECRET_KEY")!,
    FromAddress = builder.Configuration.GetValue<string>("EMAIL_FROM_ADDRESS")!,
    FromAddressName = builder.Configuration.GetValue<string>("EMAIL_FROM_ADDRESS_NAME")
};
builder.Services.AddSingleton(emailConfiguration);

var notificationConfiguration = new NotificationConfiguration()
{
    NotificationEmailAddress = builder.Configuration.GetValue<string>("EMAIL_TO_ADDRESS")!
};
builder.Services.AddSingleton(notificationConfiguration);

// Use SeriLog for ASP.NET logging
builder.Services.AddSerilog();

builder.Services.AddTransient<SESService>();
builder.Services.AddTransient<SonarrEmailBuilder>();
builder.Services.AddTransient<RadarrEmailBuilder>();
builder.Services.AddTransient<HtmlRenderer>();

builder.Services.AddSingleton<NotificationTaskQueue>();
builder.Services.AddHostedService<NotificationTaskBackgroundService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();