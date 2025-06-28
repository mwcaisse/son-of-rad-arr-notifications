using SonOfRadArrNotifications.Sonarr.Payloads.Models;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class SonarrImportPayload : SonarrPayloadBase
{
    public SonarrSeries Series { get; set; }
    
    public List<SonarrEpisode> Episodes { get; set; }
    
    public List<SonarrEpisodeFile> EpisodeFiles { get; set; }
    
    public string DownloadClient { get; set; }
    
    public string DownloadClientType { get; set; }
    
    public string DownloadId { get; set; }
    
    public SonarrGrabbedRelease Release { get; set; }
    
    public int FileCount { get; set; }
    
    public string SourcePath {get; set;}
    
    public string DestinationPath {get; set;}
    
}