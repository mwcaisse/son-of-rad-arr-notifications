using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using SonOfRadArrNotifications.Common;
using SonOfRadArrNotifications.Radarr.Payloads;
using SonOfRadArrNotifications.Radarr.Templates;
using SonOfRadArrNotifications.Sonarr.Payloads;

namespace SonOfRadArrNotifications.Radarr;

public class RadarrEmailBuilder
{
    
    private readonly HtmlRenderer _htmlRenderer;
    
    private readonly JsonSerializerOptions _serializerOptions;

    public RadarrEmailBuilder(HtmlRenderer htmlRenderer)
    {
        _htmlRenderer = htmlRenderer;
        
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
            var payload = JsonSerializer.Deserialize<RadarrPayloadBase>(rawPayloadJson, _serializerOptions);

            if (payload != null)
            {
                switch (payload.EventType)
                {
                    case RadarrEventType.Grab:
                        return await HandleGrab(rawPayloadJson);
                }
            }
            
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

    private async Task<NotificationEmail> HandleGrab(string payloadJson)
    {
        var grabbedPayload = JsonSerializer.Deserialize<RadarrGrabPayload>(payloadJson, _serializerOptions)!;
        
        var html = await RenderTemplate<GrabMovieTemplate>(new Dictionary<string, object>()
        {
            { "Payload", grabbedPayload } 
        });

        return new NotificationEmail()
        {
            Subject = CreateSubject("Movie Grabbed"),
            Body = html,
        };
    }
    
    private Task<string> RenderTemplate<T>(Dictionary<string, object> parameters) where T : IComponent
    {
        return EmailBuilderUtils.RenderTemplate<T>(_htmlRenderer, parameters);
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