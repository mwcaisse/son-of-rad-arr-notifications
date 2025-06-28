namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrGrabbedRelease
{
    public string ReleaseTitle { get; set; }
    
    public string Indexer { get; set; }
    
    public long Size { get; set; }
    
    public List<string> IndexerFlags { get; set; }
}