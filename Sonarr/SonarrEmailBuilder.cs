using System.Text.Json;
using SonOfRadArrNotifications.Sonarr.Payloads;

namespace SonOfRadArrNotifications.Sonarr;

public class SonarrEmailBuilder
{

    public NotificationEmail BuildEmailBody(string rawPayloadJson)
    {
        var payload = DeserializePayload(rawPayloadJson);

        if (payload == null)
        {
            return HandleUnknownPayload(rawPayloadJson);
        }
        
        // otherwise we shall build out the email
        switch (payload)
        {
            case SonarrGrabPayload:
                break;
            case SonarrImportPayload:
                break;
        }

        return HandleUnknownPayload(rawPayloadJson);
    }

    private NotificationEmail HandleUnknownPayload(string json)
    {
        return new NotificationEmail()
        {
            Subject = "Sonarr: Unknown Event Type",
            Body = json,
        };
    }

    /// <summary>
    /// Deserializes the given payload JSON into its corresponding Payload class
    ///
    /// Returns null if it is an unknown payload type
    /// </summary>
    /// <param name="rawPayloadJson"></param>
    /// <returns></returns>
    private SonarrPayloadBase? DeserializePayload(string rawPayloadJson)
    {
        try
        {
            return JsonSerializer.Deserialize<SonarrPayloadBase>(rawPayloadJson);
        }
        catch (NotSupportedException e)
        {
            // this means that we don't have a discriminator type for this message type, return null
            return null;
        }
    }
}