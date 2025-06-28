namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class NotificationEmail
{
    public required string Subject { get; set; }
    
    public required string Body { get; set; }
}