namespace SonOfRadArrNotifications.Sonarr.Payloads.Models;

public class SonarrSeries
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string TitleSlug { get; set; }
    
    public string Path { get; set; }
    
    public int TvdbId { get; set; }
    
    public int TvMazeId { get; set; }
    
    public int TmdbId { get; set; }
    
    public string ImdbId { get; set; }
    
    public SonarrSeriesTypes Type { get; set; }
    
    public int Year { get; set; }
    
    public List<string> Genres { get; set; }
    
    public List<SonarrImage> Images { get; set; }
    
    public List<string> Tags { get; set; }
    
    public Language OriginalLanguage { get; set; }
}