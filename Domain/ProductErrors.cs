using SharedKernel;

namespace Domain;

public static class ProductErrors
{
    public static Error NotFound(int id) => Error.NotFound(
        "Product.NotFound",
        $"The product with the Id = '{id}' was not found.");
}
