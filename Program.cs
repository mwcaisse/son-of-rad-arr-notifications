
using Serilog;
using SonOfRadArrNotifications.Configuration;
using SonOfRadArrNotifications.Services;

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
    Region = builder.Configuration.GetValue<string>("email:region")!,
    AccessKey = builder.Configuration.GetValue<string>("email:accessKey")!,
    SecretKey = builder.Configuration.GetValue<string>("email:secretKey")!,
    FromAddress = builder.Configuration.GetValue<string>("email:fromAddress")!
};
builder.Services.AddSingleton(emailConfiguration);

var notificationConfiguration = new NotificationConfiguration()
{
    NotificationEmailAddress = builder.Configuration.GetValue<string>("notification:toAddress")!
};
builder.Services.AddSingleton(notificationConfiguration);

// Use SeriLog for ASP.NET logging
builder.Services.AddSerilog();

builder.Services.AddTransient<SESService>();

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