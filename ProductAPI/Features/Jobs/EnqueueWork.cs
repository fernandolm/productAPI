using MediatR;
using ProductAPI.Jobs.Domain;
using ProductAPI.Jobs.Service;
using ProductAPI.Jobs.Worker;
using SharedKernel;
using System.Net;

namespace ProductAPI.Features.Jobs;

internal sealed class EnqueueWorkEndpoint : IEndpoint
{
    private readonly IBackgroundTaskQueue _queue;
    public sealed class WorkRequest
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Data { get; init; } = string.Empty;
    }

    public EnqueueWorkEndpoint(IBackgroundTaskQueue queue)
    {
        _queue = queue;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("jobs", async (WorkRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrWhiteSpace(request.Data))
            {
                return Result.Failure(Error.Failure(Guid.NewGuid().ToString(), "Data cannot be empty."));
            }
            await _queue.QueueWorkItemAsync(
                new WorkItem(
                        request.Id,
                        async ct => await WorkItemWorker.ProcessWorkItemAsync(request.Data, ct).ConfigureAwait(false)
                )
            ).ConfigureAwait(false);
            return Result.Success("Accepted");
        })
        .WithTags(Tags.Jobs)
        .Produces((int)HttpStatusCode.Accepted);
    }
}
