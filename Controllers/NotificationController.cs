using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SonOfRadArrNotifications.Configuration;
using SonOfRadArrNotifications.Radarr;
using SonOfRadArrNotifications.Services;
using SonOfRadArrNotifications.Sonarr;
using SonOfRadArrNotifications.Tasks;

namespace SonOfRadArrNotifications.Controllers;

[ApiController]
[Route("/notification/")]
public class NotificationController : Controller
{

    private readonly SESService _sesService;

    private readonly NotificationTaskQueue _taskQueue;
    
    private readonly SonarrEmailBuilder _sonarrEmailBuilder;
    
    private readonly RadarrEmailBuilder _radarrEmailBuilder;

    public NotificationController(SESService sesService,
        SonarrEmailBuilder sonarrEmailBuilder, RadarrEmailBuilder radarrEmailBuilder, NotificationTaskQueue taskQueue)
    {
        _sesService = sesService;
        _sonarrEmailBuilder = sonarrEmailBuilder;
        _radarrEmailBuilder = radarrEmailBuilder;
        _taskQueue = taskQueue;
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
        var bodyJson = await ReadBodyAsString();
        await _taskQueue.QueueTask(new NotificationTask()
        {
            Type = NotificationTaskType.Sonarr,
            BodyJson = bodyJson
        });

        return Created();
    }

    [HttpPost]
    [Route("sonarr/render")]
    public async Task<IActionResult> RenderSonarrNotification()
    {
        var bodyJson = await ReadBodyAsString();
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
        var bodyJson = await ReadBodyAsString();
        await _taskQueue.QueueTask(new NotificationTask()
        {
            Type = NotificationTaskType.Radarr,
            BodyJson = bodyJson
        });

        return Created();
    }
    
    [HttpPost]
    [Route("radarr/render")]
    public async Task<IActionResult> RenderRadarrNotification()
    {
        var bodyJson = await ReadBodyAsString();
        var email = await _radarrEmailBuilder.BuildEmailBody(bodyJson);

        return new ContentResult()
        {
            Content = email.Body,
            ContentType = "text/html",
            StatusCode = 200
        };
    }

    private Task<string> ReadBodyAsString()
    {
        var bodyReader = new StreamReader(Request.Body);
        return bodyReader.ReadToEndAsync();
    }
    
}