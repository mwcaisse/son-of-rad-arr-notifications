namespace SonOfRadArrNotifications.Radarr.Payloads;

public class RadarrHealthPayload : RadarrPayloadBase
{
    public string? Level { get; set; }
    
    public string? Type { get; set; }
    
    public string? Message { get; set; }
    
    public string? WikiUrl { get; set; }
}