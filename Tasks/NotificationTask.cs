namespace SonOfRadArrNotifications.Tasks;

public class NotificationTask
{
    public required NotificationTaskType Type { get; set; }
    
    public required string BodyJson { get; set; }
}