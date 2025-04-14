using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Features.Products.Delete;

internal sealed class DeleteProductHandler
    : ICommandHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await _context.Products
            .SingleOrDefaultAsync(productFromDatabase => productFromDatabase.Id == command.Id, cancellationToken)
            .ConfigureAwait(false);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(command.Id));
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
