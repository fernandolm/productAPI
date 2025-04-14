using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Features.Products.GetById;

internal sealed class GetProductByIdHandler
    : IQueryHandler<GetProductByIdQuery, SingleProductResponse>
{
    private readonly IApplicationDbContext _context;
    public GetProductByIdHandler(IApplicationDbContext context)
    {
        this._context = context;
    }
    public async Task<Result<SingleProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        SingleProductResponse? productResponse = await this._context.Products
            .Where(product => product.Id == query.Id)
            .Select(product => new SingleProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                UpdatedAt = product.UpdatedAt
            })
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (productResponse is null)
        {
            return Result.Failure<SingleProductResponse>(ProductErrors.NotFound(query.Id));
        }

        return productResponse;
    }
}
