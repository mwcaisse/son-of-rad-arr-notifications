using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SonOfRadArrNotifications.Configuration;
using SonOfRadArrNotifications.Radarr;
using SonOfRadArrNotifications.Services;
using SonOfRadArrNotifications.Sonarr;

namespace SonOfRadArrNotifications.Controllers;

[ApiController]
[Route("/notification/")]
public class NotificationController : Controller
{

    private readonly SESService _sesService;

    private readonly NotificationConfiguration _notificationConfiguration;
    
    private readonly SonarrEmailBuilder _sonarrEmailBuilder;
    
    private readonly RadarrEmailBuilder _radarrEmailBuilder;

    public NotificationController(SESService sesService, NotificationConfiguration notificationConfiguration,
        SonarrEmailBuilder sonarrEmailBuilder, RadarrEmailBuilder radarrEmailBuilder)
    {
        _sesService = sesService;
        _notificationConfiguration = notificationConfiguration;
        _sonarrEmailBuilder = sonarrEmailBuilder;
        _radarrEmailBuilder = radarrEmailBuilder;
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
        var email = await _sonarrEmailBuilder.BuildEmailBody(bodyJson);
        
        await _sesService.SendEmail(_notificationConfiguration.NotificationEmailAddress, email.Subject, email.Body);
        return Ok();
    }

    [HttpPost]
    [Route("sonarr/render")]
    public async Task<IActionResult> RenderSonarrNotification()
    {
        var bodyReader = new StreamReader(Request.Body);
        var bodyJson = await bodyReader.ReadToEndAsync();
        var email = await _sonarrEmailBuilder.BuildEmailBody(bodyJson);

        return new ContentResult()
        {
            Content = email.Body,
            ContentType = "text/html",
            StatusCode = 200
        };
    }

    [HttpPost]
    [Route("radarr")]
    public async Task<IActionResult>  RadarrNotification()
    {
        var bodyReader = new StreamReader(Request.Body);
        var bodyJson = await bodyReader.ReadToEndAsync();
        var email = await _radarrEmailBuilder.BuildEmailBody(bodyJson);
        
        await _sesService.SendEmail(_notificationConfiguration.NotificationEmailAddress, email.Subject, email.Body);
        return Ok();
    }
    
    [HttpPost]
    [Route("radarr/render")]
    public async Task<IActionResult> RenderRadarrNotification()
    {
        var bodyReader = new StreamReader(Request.Body);
        var bodyJson = await bodyReader.ReadToEndAsync();
        var email = await _radarrEmailBuilder.BuildEmailBody(bodyJson);

        return new ContentResult()
        {
            Content = email.Body,
            ContentType = "text/html",
            StatusCode = 200
        };
    }
    
}