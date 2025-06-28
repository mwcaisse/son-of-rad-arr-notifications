using SonOfRadArrNotifications.Sonarr.Payloads.Models;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class SonarrGrabPayload : SonarrPayloadBase
{
    public SonarrSeries Series { get; set; }
    
    public List<SonarrEpisode> Episodes { get; set; }
    
    public SonarrRelease Release { get; set; }
    
    public string DownloadClient { get; set; }
    
    public string DownloadClientType { get; set; }
    
    public string DownloadId { get; set; }
    
    public SonarrCustomFormatInfo CustomFormatInfo { get; set; }
}