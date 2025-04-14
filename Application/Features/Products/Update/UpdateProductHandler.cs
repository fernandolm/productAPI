using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using SharedKernel;

namespace Application.Features.Products.Update;

internal sealed class UpdateProductHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _context.Products.Update(new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            UpdatedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
