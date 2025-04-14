using Application.Features.Products.Create;
using MediatR;

namespace ProductAPI.Features.Products;

internal sealed class CreateProductEndpoint : IEndpoint
{
    public sealed class CreateRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Stock { get; init; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateProductCommand command = new CreateProductCommand
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };
            return await sender.Send(command, cancellationToken).ConfigureAwait(false);
        })
        .WithTags(Tags.Products);
    }
}
