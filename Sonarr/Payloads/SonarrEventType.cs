using System.Text.Json.Serialization;

namespace SonOfRadArrNotifications.Sonarr.Payloads;

[JsonConverter(typeof(JsonStringEnumConverter))]
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