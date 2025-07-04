using SonOfRadArrNotifications.Sonarr.Payloads.Models;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public class SonarrSeriesAddPayload : SonarrPayloadBase
{
    public SonarrSeries Series { get; set; }
}