namespace SonOfRadArrNotifications.Sonarr.Payloads.Models;

public class SonarrCustomFormatInfo
{
    public List<SonarrCustomFormat> CustomFormats { get; set; }
    
    public int CustomFormatScore { get; set; }
}