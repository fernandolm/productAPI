using Application.Features.Products.GetAll;
using MediatR;

namespace ProductAPI.Features.Products;

internal sealed class GetAllProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender, CancellationToken cancellationToken) =>
        {
            return await sender.Send(new GetAllProductsQuery(), cancellationToken).ConfigureAwait(false);
        })
        .WithTags(Tags.Products);
    }
}
