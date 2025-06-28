namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrMovie
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public int Year { get; set; }
    
    public string FilePath { get; set; }
    
    public string ReleaseDate { get; set; }
    
    public string FolderPath { get; set; }
    
    public int TmdbId { get; set; }
    
    public string ImdbId { get; set; }
    
    public string Overview { get; set; }
    
    public List<string> Genres { get; set; }
    
    public List<RadarrImage> Images { get; set; }
    
    public List<string> Tags { get; set; }
    
    public Language OriginalLanguage { get; set; }
}