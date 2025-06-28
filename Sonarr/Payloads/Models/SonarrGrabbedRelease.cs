namespace SonOfRadArrNotifications.Sonarr.Payloads.Models;

public class SonarrGrabbedRelease
{
    public string ReleaseTitle { get; set; }
    
    public string Indexer { get; set; }
    
    public long Size { get; set; }
    
    public List<string> IndexerFlags { get; set; }
    
    public SonarrReleaseType ReleaseType { get; set; }
}