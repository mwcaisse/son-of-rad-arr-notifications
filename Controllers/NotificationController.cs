using Microsoft.AspNetCore.Mvc;

namespace SonOfRadArrNotifications.Controllers;

[ApiController]
[Route("/notification/")]
public class NotificationController
{

    /*
     * Take a notification from Sonarr
     *
     *      Construct an email
     *      send to SES
     */
    [HttpPost]
    [Route("sonarr")]
    public void SonarrNotification()
    {
        
    }

    [HttpPost]
    [Route("radarr")]
    public void RadarrNotification()
    {
        
    }
    
}