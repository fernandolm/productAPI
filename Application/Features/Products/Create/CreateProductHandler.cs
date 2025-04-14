using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using SharedKernel;

namespace Application.Features.Products.Create;

internal sealed class CreateProductHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            UpdatedAt = DateTime.UtcNow
        }).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
