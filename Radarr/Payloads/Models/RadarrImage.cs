namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrImage
{
    public RadarrMediaCoverTypes CoverType { get; set; }
    
    public string Url { get; set; }
    
    public string RemoteUrl { get; set; }
}