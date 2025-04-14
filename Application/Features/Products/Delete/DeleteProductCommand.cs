using Application.Abstractions.Messaging;

namespace Application.Features.Products.Delete;

public sealed record DeleteProductCommand(int Id) : ICommand
{
}
