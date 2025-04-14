using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ProductAPI.Jobs.Domain;

namespace ProductAPI.Jobs.Service;

public sealed class BackgroundProcessingService : BackgroundService
{
    private readonly IBackgroundTaskQueue _queue;
    private readonly IMemoryCache _cache;
    private readonly ProcessingSettings _settings;
    private int _activeWorkers;
    private long _processedCount;

    public BackgroundProcessingService(
        IBackgroundTaskQueue queue,
        IMemoryCache cache,
        IOptions<ProcessingSettings> options)
    {
        _queue = queue;
        _cache = cache;
        _settings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var workers = new List<Task>(_settings.MaxConcurrency);
        for (int i = 0; i < _settings.MaxConcurrency; i++)
        {
            workers.Add(ProcessQueueAsync(ct));
        }
        await Task.WhenAll(workers).ConfigureAwait(false);
    }

    private async Task ProcessQueueAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var work = await _queue.DequeueAsync(ct).ConfigureAwait(false);
            Interlocked.Increment(ref _activeWorkers);

            try
            {
                if (!_cache.TryGetValue(work.Id, out _))
                {
                    using var entry = _cache.CreateEntry(work.Id);
                    entry.SetOptions(new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = _settings.CacheExpiration
                    });

                    await work.Task(ct).ConfigureAwait(false);
                    entry.Value = true; // Mark as processed
                }
            }
            finally
            {
                Interlocked.Decrement(ref _activeWorkers);
                Interlocked.Increment(ref _processedCount);
            }
        }
    }

    public ProcessingMetrics GetMetrics() => new(
        Interlocked.CompareExchange(ref _activeWorkers, 0, 0),
        Interlocked.Read(ref _processedCount),
        _queue.GetQueuedCount());
}
