namespace SonOfRadArrNotifications.Radarr.Payloads.Models;

public class RadarrCustomFormatInfo
{
    public List<RadarrCustomFormat> CustomFormats { get; set; }
    
    public int CustomFormatScore { get; set; }
}