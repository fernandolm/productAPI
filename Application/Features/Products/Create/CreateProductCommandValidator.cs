using Application.Abstractions.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters")
            .MustAsync(BeUniqueProductName).WithMessage("Product name must be unique");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero");
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
    }
    private async Task<bool> BeUniqueProductName(string name, CancellationToken cancellationToken)
    {
        return !await _dbContext.Products.AnyAsync(p => p.Name == name, cancellationToken).ConfigureAwait(false);
    }
}
