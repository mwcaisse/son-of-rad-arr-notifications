using SonOfRadArrNotifications.Sonarr.Payloads.Models;

namespace SonOfRadArrNotifications.Sonarr.Payloads;


public class SonarrImportPayload : SonarrPayloadBase
{
    public SonarrSeries Series { get; set; }
    
    public List<SonarrEpisode> Episodes { get; set; }
    
    /// <summary>
    /// Sonarr has two events that use the "Download" event type, File Import and Import Complete
    ///
    /// One of them will have a list of EpisodeFiles while one will have a singular Episode File.
    ///     We will add both of them so that we can handle both payload types
    /// </summary>
    public List<SonarrEpisodeFile> EpisodeFiles { get; set; }
    
    public SonarrEpisodeFile? EpisodeFile { get; set; }
    
    public string DownloadClient { get; set; }
    
    public string DownloadClientType { get; set; }
    
    public string DownloadId { get; set; }
    
    public SonarrGrabbedRelease Release { get; set; }
    
    public int FileCount { get; set; }
    
    public string SourcePath {get; set;}
    
    public string DestinationPath {get; set;}

    public SonarrEpisodeFile? GetFirstEpisodeFile()
    {
        if (EpisodeFile != null)
        {
            return EpisodeFile;
        }
        if (EpisodeFiles.Count > 0)
        {
            return EpisodeFiles[0];
        }

        return null;
    }
    
}