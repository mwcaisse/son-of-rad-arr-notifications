using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SonOfRadArrNotifications.Configuration;
using SonOfRadArrNotifications.Services;

namespace SonOfRadArrNotifications.Controllers;

[ApiController]
[Route("/notification/")]
public class NotificationController : Controller
{

    private readonly SESService _sesService;

    private readonly NotificationConfiguration _notificationConfiguration;

    public NotificationController(SESService sesService, NotificationConfiguration notificationConfiguration)
    {
        _sesService = sesService;
        _notificationConfiguration = notificationConfiguration;
    }

    /*
     * Take a notification from Sonarr
     *
     *      Construct an email
     *      send to SES
     */
    [HttpPost]
    [Route("sonarr")]
    public async Task<IActionResult> SonarrNotification()
    {
        var bodyReader = new StreamReader(Request.Body);
        var bodyJson = await bodyReader.ReadToEndAsync();
        await _sesService.SendEmail(_notificationConfiguration.NotificationEmailAddress, "Test Notification", bodyJson);
        return Ok();
    }

    [HttpPost]
    [Route("radarr")]
    public void RadarrNotification()
    {
        
    }
    
}