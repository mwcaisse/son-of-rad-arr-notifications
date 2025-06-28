namespace SonOfRadArrNotifications.Sonarr.Templates;

public static class TemplateUtils
{
    public static string CreateSeasonEpisodeString(int season, int episode)
    {
        var seasonNumber =  season.ToString("D2");
        var episodeNumber =  episode.ToString("D2");

        return $"S{seasonNumber}-E{episodeNumber}";
    }
}