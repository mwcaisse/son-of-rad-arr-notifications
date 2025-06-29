using Serilog;
using SonOfRadArrNotifications.Configuration;
using SonOfRadArrNotifications.Radarr;
using SonOfRadArrNotifications.Services;
using SonOfRadArrNotifications.Sonarr;
using SonOfRadArrNotifications.Sonarr.Payloads;

namespace SonOfRadArrNotifications.Tasks;

public class NotificationTaskBackgroundService : BackgroundService
{
    
    private readonly IServiceProvider _serviceProvider;
    
    private readonly NotificationTaskQueue _notificationTaskQueue;

    public NotificationTaskBackgroundService(IServiceProvider serviceProvider, NotificationTaskQueue notificationTaskQueue)
    {
        _serviceProvider = serviceProvider;
        _notificationTaskQueue = notificationTaskQueue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (!await _notificationTaskQueue.WaitForTask(stoppingToken))
                {
                    // If this returns false, it means the channel has closed, and we can never read from the channel again
                    Log.Warning("Notification task queue channel closed. Aborting.");
                    break;
                }
                
                using var scope = _serviceProvider.CreateScope();
                while (_notificationTaskQueue.ReadTask(out NotificationTask? task) &&
                       !stoppingToken.IsCancellationRequested)
                {
                    await ProcessNotificationTask(scope, task!);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while processing notification task.");
            }
        }
    }

    private async Task ProcessNotificationTask(IServiceScope scope, NotificationTask task)
    {
        var email = await CreateEmailForTask(scope, task);
        
        var configuration = scope.ServiceProvider.GetRequiredService<NotificationConfiguration>();
        var emailService = scope.ServiceProvider.GetRequiredService<SESService>();
        await emailService.SendEmail(configuration.NotificationEmailAddress, email.Subject, email.Body);
    }

    private Task<NotificationEmail> CreateEmailForTask(IServiceScope scope, NotificationTask task)
    {
        switch (task.Type)
        {
            case NotificationTaskType.Radarr:
                var radarrEmailBuilder = scope.ServiceProvider.GetRequiredService<RadarrEmailBuilder>();
                return radarrEmailBuilder.BuildEmailBody(task.BodyJson);
            case NotificationTaskType.Sonarr:
                var sonarrEmailBuilder = scope.ServiceProvider.GetRequiredService<SonarrEmailBuilder>();
                return sonarrEmailBuilder.BuildEmailBody(task.BodyJson);;
        }
        
        throw new Exception("Unknown notification type");
    }
}