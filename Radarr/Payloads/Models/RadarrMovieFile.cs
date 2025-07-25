namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrMovieFile
{
    public int Id { get; set; }
    
    public string RelativePath { get; set; }
    
    public string Path { get; set; }
    
    public string Quality { get; set; }
    
    public int QualityVersion { get; set; }
    
    public string ReleaseGroup { get; set; }
    
    public string SceneName { get; set; }
    
    public string IndexerFlags { get; set; }
    
    public long Size { get; set; }
    
    public DateTime? DateAdded { get; set; }
    
    public List<Language> Languages { get; set; }
    
    public RadarrMovieFileMediaInfo? MediaInfo { get; set; }
    
    public string SourcePath { get; set; }
    
    public string RecycleBinPath { get; set; }
}