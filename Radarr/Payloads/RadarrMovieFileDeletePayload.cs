using SonOfRadArrNotifications.Radarr.Payloads.Models;

namespace SonOfRadArrNotifications.Radarr.Payloads;

public class RadarrMovieFileDeletePayload : RadarrPayloadBase
{
    public RadarrMovie Movie { get; set; }
    
    public RadarrMovieFile MovieFile { get; set; }
    
    public DeleteMediaFileReason DeleteReason { get; set; }
}