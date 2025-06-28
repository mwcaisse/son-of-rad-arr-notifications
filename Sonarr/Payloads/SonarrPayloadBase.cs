using System.Text.Json.Serialization;

namespace SonOfRadArrNotifications.Sonarr.Payloads;


[JsonPolymorphic(TypeDiscriminatorPropertyName = "eventType")]
[JsonDerivedType(typeof(SonarrImportPayload), typeDiscriminator: "Download")]
[JsonDerivedType(typeof(SonarrGrabPayload), typeDiscriminator: "Grab")]
public class SonarrPayloadBase
{
    public SonarrEventType EventType { get; set; }
    
    public string InstanceName { get; set; }
    
    public string ApplicationUrl { get; set; }
}