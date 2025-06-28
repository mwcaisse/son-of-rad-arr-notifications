namespace SonOfRadArrNotifications.Configuration;

public class EmailConfiguration
{
    public required string AccessKey { get; init; }
    
    public required string SecretKey { get; init; }
    
    public required string Region { get; init; }
    
    public required string FromAddress { get; init; }
    
    public string? FromAddressName { get; init; }
}