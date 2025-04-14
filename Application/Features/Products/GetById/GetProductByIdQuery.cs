using Application.Abstractions.Messaging;

namespace Application.Features.Products.GetById;

public sealed record GetProductByIdQuery(int Id) : IQuery<SingleProductResponse>;
