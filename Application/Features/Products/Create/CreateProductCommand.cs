using Application.Abstractions.Messaging;

namespace Application.Features.Products.Create;

public record CreateProductCommand : ICommand
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Stock { get; init; }
}
