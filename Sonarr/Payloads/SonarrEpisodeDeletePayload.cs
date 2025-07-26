using SonOfRadArrNotifications.Common.Models;
using SonOfRadArrNotifications.Sonarr.Payloads.Models;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class SonarrEpisodeDeletePayload : SonarrPayloadBase
{
    public SonarrSeries Series { get; set; }
    
    public List<SonarrEpisode> Episodes { get; set; } 
    
    public SonarrEpisodeFile EpisodeFile { get; set; }
    
    public DeleteMediaFileReason DeleteReason { get; set; }
}