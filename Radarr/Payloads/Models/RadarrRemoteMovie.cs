namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrRemoteMovie
{
    public int TmdbId { get; set; }
    
    public string ImdbId { get; set; }
    
    public string Title { get; set; }
    
    public int Year { get; set; }
}