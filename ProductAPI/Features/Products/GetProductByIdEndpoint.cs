using Application.Features.Products.GetById;
using MediatR;

namespace ProductAPI.Features.Products;

internal sealed class GetProductByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            return await sender.Send(new GetProductByIdQuery(id), cancellationToken).ConfigureAwait(false);
        })
        .WithTags(Tags.Products);
    }
}
