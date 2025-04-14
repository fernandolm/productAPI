namespace ProductAPI.Jobs.Domain;

public class ProcessingSettings
{
    public int MaxConcurrency { get; set; } = 4;
    public TimeSpan CacheExpiration { get; set; } = TimeSpan.FromMinutes(10);
    public int QueueCapacity { get; set; } = 100;
}