using ProductAPI.Jobs.Domain;

namespace ProductAPI.Jobs.Service;

public interface IBackgroundTaskQueue
{
    ValueTask QueueWorkItemAsync(WorkItem work);
    ValueTask<WorkItem> DequeueAsync(CancellationToken ct);
    int GetQueuedCount();
}
