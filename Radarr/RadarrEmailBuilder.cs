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
                        return await RenderTemplate<GrabMovieTemplate, RadarrGrabPayload>(rawPayloadJson, "Movie Grabbed");
                    case RadarrEventType.Download:
                        return await RenderTemplate<DownloadMovieTemplate, RadarrImportPayload>(rawPayloadJson, "Movie Imported");
                    case RadarrEventType.MovieFileDelete:
                        return await RenderTemplate<MovieFileDeleteTemplate, RadarrMovieFileDeletePayload>(rawPayloadJson, "Movie File Deleted");
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
    
    private async Task<NotificationEmail> RenderTemplate<TT, TP>(string payloadJson, string subject, string payloadParamName = "Payload") where TT : IComponent where TP : RadarrPayloadBase
    {
        var payload = JsonSerializer.Deserialize<TP>(payloadJson, _serializerOptions)!;
        var parameters = new Dictionary<string, object>()
        {
            { payloadParamName, payload }
        };
        
        var html = await EmailBuilderUtils.RenderTemplate<TT>(_htmlRenderer, parameters);
        return new NotificationEmail()
        {
            Subject = CreateSubject(subject),
            Body = html
        };
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