using SonOfRadArrNotifications.Sonarr.Payloads.Models;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class SonarrImportPayload : SonarrPayloadBase
{
    
    public SonarrSeries Series { get; set; }
    
    public List<SonarrEpisode> Episodes { get; set; }
    
}