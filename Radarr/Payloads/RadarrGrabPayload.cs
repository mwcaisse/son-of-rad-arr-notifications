using SonOfRadArrNotifications.Radarr.Payloads.Models;

namespace SonOfRadArrNotifications.Radarr.Payloads;

public class RadarrGrabPayload : RadarrPayloadBase
{
    public RadarrMovie Movie { get; set; }
    
    public RadarrRemoteMovie RemoteMovie { get; set; }
    
    public RadarrRelease Release { get; set; }
    
    public string DownloadClient { get; set; }
    
    public string DownloadClientType { get; set; }
    
    public string DownloadId { get; set; }
    
    public RadarrCustomFormatInfo CustomFormatInfo { get; set; }
}