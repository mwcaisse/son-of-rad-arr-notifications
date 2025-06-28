using SonOfRadArrNotifications.Radarr.Payloads.Models;

namespace SonOfRadArrNotifications.Radarr.Payloads;

public class RadarrImportPayload : RadarrPayloadBase
{
    public RadarrMovie Movie { get; set; }
    
    public RadarrRemoteMovie RemoteMovie { get; set; }
    
    public RadarrMovieFile MovieFile { get; set; }
    
    public bool IsUpgrade { get; set; }
    
    public string DownloadClient { get; set; }
    
    public string DownloadClientType { get; set; }
    
    public string DownloadId { get; set; }
    
    public List<RadarrMovieFile> DeletedFiles { get; set; }
    
    public RadarrCustomFormatInfo CustomFormatInfo { get; set; }
    
    public RadarrGrabbedRelease Release { get; set; }
}