namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrRelease
{
    public string Quality { get; set; }
    
    public int QualityVersion { get; set; }
    
    public string ReleaseGroup { get; set; }
    
    public string ReleaseTitle { get; set; }
    
    public string Indexer { get; set; }
    
    public long Size { get; set; }
    
    public int CustomFormatScore { get; set; }
    
    public List<string> CustomFormats { get; set; }
    
    public List<Language> Languages { get; set; }
    
    public List<string> IndexerFlags { get; set; }
}