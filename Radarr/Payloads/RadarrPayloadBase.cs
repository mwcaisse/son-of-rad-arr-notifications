namespace SonOfRadArrNotifications.Radarr.Payloads;

public class RadarrPayloadBase
{
    public RadarrEventType EventType { get; set; }
    
    public string? InstanceName { get; set; }
    
    public string? ApplicationUrl { get; set; }
}