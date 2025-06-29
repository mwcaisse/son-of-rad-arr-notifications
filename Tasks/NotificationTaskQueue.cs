using System.Threading.Channels;

namespace SonOfRadArrNotifications.Tasks;

public class NotificationTaskQueue
{

    private readonly Channel<NotificationTask> _taskChannel;

    public NotificationTaskQueue()
    {
        _taskChannel = Channel.CreateUnbounded<NotificationTask>(new UnboundedChannelOptions()
        {
            SingleReader = true,
            SingleWriter = false
        });
    }

    public ValueTask QueueTask(NotificationTask task)
    {
        return _taskChannel.Writer.WriteAsync(task);
    }

    public ValueTask<bool> WaitForTask(CancellationToken cancellationToken)
    {
        return _taskChannel.Reader.WaitToReadAsync(cancellationToken);
    }

    public bool ReadTask(out NotificationTask? task)
    {
        return _taskChannel.Reader.TryRead(out task);
    }
}