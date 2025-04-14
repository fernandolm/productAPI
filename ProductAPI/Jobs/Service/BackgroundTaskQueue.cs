using ProductAPI.Jobs.Domain;
using System.Threading.Channels;

namespace ProductAPI.Jobs.Service;

public sealed class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<WorkItem> _queue;
    public BackgroundTaskQueue(int capacity)
    {
        BoundedChannelOptions options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<WorkItem>(options);
    }

    public async ValueTask QueueWorkItemAsync(WorkItem work)
        => await _queue.Writer.WriteAsync(work).ConfigureAwait(false);
    public async ValueTask<WorkItem> DequeueAsync(CancellationToken ct)
        => await _queue.Reader.ReadAsync(ct).ConfigureAwait(false);
    public int GetQueuedCount() => _queue.Reader.Count;
}
