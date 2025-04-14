namespace ProductAPI.Jobs.Domain;

public record WorkItem(Guid Id, Func<CancellationToken, ValueTask> Task);
