using System.Text.Json.Serialization;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class SonarrPayloadBase
{
    public SonarrEventType EventType { get; set; }
    
    public string InstanceName { get; set; }
    
    public string ApplicationUrl { get; set; }
}