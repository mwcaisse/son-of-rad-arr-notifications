namespace SonOfRadArrNotifications.Sonarr.Payloads.Models;

public class SonarrImage
{
    public MediaCoverTypes CoverType { get; set; }
    
    public string Url { get; set; }
    
    public string RemoteUrl { get; set; }
}