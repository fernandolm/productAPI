using Application.Abstractions.Messaging;

namespace Application.Features.Products.GetAll;

public sealed record GetAllProductsQuery() : IQuery<List<ProductResponse>>;
