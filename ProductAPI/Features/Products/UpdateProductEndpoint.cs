using Application.Features.Products.Update;
using MediatR;

namespace ProductAPI.Features.Products;

internal sealed class UpdateProductEndpoint : IEndpoint
{
    public sealed class UpdateRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Stock { get; init; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:int}", async (int id, UpdateRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            UpdateProductCommand command = new UpdateProductCommand
            {
                Id = id,
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
