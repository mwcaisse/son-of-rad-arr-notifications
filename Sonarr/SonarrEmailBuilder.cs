using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using SonOfRadArrNotifications.Common;
using SonOfRadArrNotifications.Sonarr.Payloads;
using SonOfRadArrNotifications.Sonarr.Templates;

namespace SonOfRadArrNotifications.Sonarr;

public class SonarrEmailBuilder
{

    private readonly HtmlRenderer _htmlRenderer;

    private readonly JsonSerializerOptions _serializerOptions;

    public SonarrEmailBuilder(HtmlRenderer htmlRenderer)
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
            // The JsonSerializer expects the discriminator type to be the first field in the payload
            //      we can't guarantee that, so we deserialize it first to base type to get the type
            //      then deserialize it as the proper type once we know what it is.
            var payload = JsonSerializer.Deserialize<SonarrPayloadBase>(rawPayloadJson, _serializerOptions);

            if (payload != null)
            {
                switch (payload.EventType)
                {
                    case SonarrEventType.Grab:
                        return await HandleGrab(rawPayloadJson);
                    case SonarrEventType.Download:
                        return await HandleDownload(rawPayloadJson);
                    case SonarrEventType.SeriesAdd:
                        return await HandleSeriesAdd(rawPayloadJson);
                    case SonarrEventType.EpisodeFileDelete:
                        return await HandleEpisodeDeleted(rawPayloadJson);
                }
            }

            return HandleUnknownPayload(rawPayloadJson);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error parsing sonar payload");
            return new NotificationEmail()
            {
                Subject = CreateSubject("Failed to parse payload"),
                Body = rawPayloadJson + "\n\n\n" + e.Message + "\n\n" + e.StackTrace,
            };
        }
        
    }

    private Task<string> RenderTemplate<T>(Dictionary<string, object> parameters) where T : IComponent
    {
        return EmailBuilderUtils.RenderTemplate<T>(_htmlRenderer, parameters);
    }

    private async Task<NotificationEmail> HandleGrab(string payloadJson)
    {
        var grabbedPayload = JsonSerializer.Deserialize<SonarrGrabPayload>(payloadJson, _serializerOptions)!;

        var html = await RenderTemplate<GrabEpisodeTemplate>(new Dictionary<string, object>()
        {
            { "Payload", grabbedPayload }
        });
        
        return new NotificationEmail()
        {
            Subject = CreateSubject("Episode Grabbed"),
            Body = html
        };
    }

    private async Task<NotificationEmail> HandleSeriesAdd(string payloadJson)
    {
        var payload = JsonSerializer.Deserialize<SonarrSeriesAddPayload>(payloadJson, _serializerOptions)!;

        var html = await RenderTemplate<SeriesAddTemplate>(new Dictionary<string, object>()
        {
            { "Payload", payload }
        });
        
        return new NotificationEmail()
        {
            Subject = CreateSubject("New Show Added"),
            Body = html
        };
    }

    private async Task<NotificationEmail> HandleDownload(string payloadJson)
    {
        var downloadPayload = JsonSerializer.Deserialize<SonarrImportPayload>(payloadJson, _serializerOptions)!;

        var html = await RenderTemplate<DownloadEpisodeTemplate>(new Dictionary<string, object>()
        {
            { "Payload", downloadPayload }
        });

        return new NotificationEmail()
        {
            Subject = CreateSubject("Episode Imported"),
            Body = html,
        };
    }

    private async Task<NotificationEmail> HandleEpisodeDeleted(string payloadJson)
    {
        var episodeFileDeletedPayload = JsonSerializer.Deserialize<SonarrEpisodeDeletePayload>(payloadJson, _serializerOptions)!;
        
        var html = await RenderTemplate<EpisodeDeleteTemplate>(new Dictionary<string, object>()
        {
            { "Payload", episodeFileDeletedPayload }
        });

        return new NotificationEmail()
        {
            Subject = CreateSubject("Episode File Deleted"),
            Body = html,
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
        return $"Sonarr: {eventName}";
    }
}