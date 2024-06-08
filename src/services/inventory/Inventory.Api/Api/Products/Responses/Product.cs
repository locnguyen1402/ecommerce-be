using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description,
    List<ProductVariantResponse> Variants
);

public static class ProductProjection
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return ToProductResponse().Compile().Invoke(product);
    }

    public static Expression<Func<Product, ProductResponse>> ToProductResponse()
        => x =>
        new ProductResponse(
            x.Id,
            x.Name,
            x.Slug,
            x.Description,
            x.ProductVariants
                .AsQueryable()
                .Select(ProductVariantProjection.ToProductVariantResponse())
                .ToList()
        );
}