using System.Text.Json.Serialization;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

public enum SonarrEventType
{
    Test,
    Grab,
    Download,
    Rename,
    SeriesAdd,
    SeriesDelete,
    EpisodeFileDelete,
    Health,
    ApplicationUpdate,
    HealthRestored,
    ManualInteractionRequired
}