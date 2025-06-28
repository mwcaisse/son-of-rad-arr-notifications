using System.Text.Json;
using SonOfRadArrNotifications.Sonarr.Payloads;

namespace SonOfRadArrNotifications.Sonarr;

public class SonarrEmailBuilder
{

    public NotificationEmail BuildEmailBody(string rawPayloadJson)
    {
        try
        {
            // The JsonSerializer expects the discriminator type to be the first field in the payload
            //      we can't guarantee that, so we deserialize it first to base type to get the type
            //      then deserialize it as the proper type once we know what it is.
            var payload = JsonSerializer.Deserialize<SonarrPayloadBase>(rawPayloadJson);

            if (payload != null)
            {
                switch (payload.EventType)
                {
                    // TODO: reserialize it into the appropriate type
                    case SonarrEventType.Grab:
                        break;
                    case SonarrEventType.Download:
                        break;
                }
            }

            return HandleUnknownPayload(rawPayloadJson);
        }
        catch (Exception e)
        {
            return new NotificationEmail()
            {
                Subject = "Sonarr: Failed to parse payload",
                Body = rawPayloadJson + "\n\n\n" + e.Message + "\n\n" + e.StackTrace,
            };
        }
        
    }

    private NotificationEmail HandleUnknownPayload(string json)
    {
        return new NotificationEmail()
        {
            Subject = "Sonarr: Unknown Event Type",
            Body = json,
        };
    }
}