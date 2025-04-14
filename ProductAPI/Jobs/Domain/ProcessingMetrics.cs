namespace ProductAPI.Jobs.Domain;

public record ProcessingMetrics(
    int ActiveWorkers,
    long TotalProcessed,
    int QueueDepth)
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    public string Status => ActiveWorkers > 0 ? "Active" : "Idle";
}