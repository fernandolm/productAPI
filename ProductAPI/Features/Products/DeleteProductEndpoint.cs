using Application.Features.Products.Delete;
using MediatR;

namespace ProductAPI.Features.Products;

internal sealed class DeleteProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            return await sender.Send(new DeleteProductCommand(id), cancellationToken).ConfigureAwait(false);
        })
        .WithTags(Tags.Products);
    }
}