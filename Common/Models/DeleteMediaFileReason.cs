namespace SonOfRadArrNotifications.Common.Models;

public enum DeleteMediaFileReason
{
    MissingFromDisk,
    Manual,
    Upgrade,
    NoLinkedEpisodes,
    ManualOverride
}

public static class DeleteMediaFileReasonExtensions
{
    public static string FriendlyName(this DeleteMediaFileReason reason)
    {
        switch (reason)
        {
            case DeleteMediaFileReason.MissingFromDisk:
                return "Missing from disk";
            case DeleteMediaFileReason.Manual:
                return "Manual";
            case DeleteMediaFileReason.Upgrade:
                return "Upgrade";
            case DeleteMediaFileReason.NoLinkedEpisodes:
                return "No linked episodes";
            case DeleteMediaFileReason.ManualOverride:
                return "Manual override";
            default:
                throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
        }
    }
}