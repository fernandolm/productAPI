using MediatR;
using ProductAPI.Jobs.Service;
using SharedKernel;
using System.Text.Json;

namespace ProductAPI.Features.Jobs;

internal sealed class GetMetricsEndpoint : IEndpoint
{
    private readonly BackgroundProcessingService _backgroundProcessingService;
    public GetMetricsEndpoint(BackgroundProcessingService backgroundProcessingService)
    {
        _backgroundProcessingService = backgroundProcessingService;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("jobs", async (ISender sender, CancellationToken cancellationToken) =>
        {
            return Result.Success(JsonSerializer.Serialize(_backgroundProcessingService.GetMetrics()));
        })
        .WithTags(Tags.Jobs);
    }
}
