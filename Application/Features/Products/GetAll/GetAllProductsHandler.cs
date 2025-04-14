using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Features.Products.GetAll;

internal sealed class GetAllProductsHandler
    : IQueryHandler<GetAllProductsQuery, List<ProductResponse>>
{
    private readonly IApplicationDbContext _context;
    public GetAllProductsHandler(IApplicationDbContext context)
    {
        this._context = context;
    }
    public async Task<Result<List<ProductResponse>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        return await this._context.Products
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                UpdatedAt = product.UpdatedAt
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
