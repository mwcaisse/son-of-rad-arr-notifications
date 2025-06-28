using System.Text.Json;
using System.Text.Json.Serialization;
using Serilog;
using SonOfRadArrNotifications.Sonarr.Payloads;

namespace SonOfRadArrNotifications.Radarr;

public class RadarrEmailBuilder
{
    
    private readonly JsonSerializerOptions _serializerOptions;

    public RadarrEmailBuilder()
    {
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }

    public async Task<NotificationEmail> BuildEmailBody(string rawPayloadJson)
    {
        try
        {
            return HandleUnknownPayload(rawPayloadJson);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error parsing radarr payload");
            return new NotificationEmail()
            {
                Subject = CreateSubject("Failed to parse payload"),
                Body = rawPayloadJson + "\n\n\n" + e.Message + "\n\n" + e.StackTrace,
            };
        }
        
    }
    
    private NotificationEmail HandleUnknownPayload(string json)
    {
        return new NotificationEmail()
        {
            Subject = CreateSubject("Unknown Event Type"),
            Body = json,
        };
    }

    private static string CreateSubject(string eventName)
    {
        return $"Radarr: {eventName}";
    }
    
}