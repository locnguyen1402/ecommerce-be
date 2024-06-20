using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description,
    List<ProductAttributeResponse> Attributes,
    List<ProductVariantResponse> Variants
)
{
    public decimal Price => Variants.Min(x => x.Price);
};

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
            x.ProductAttributes
                .AsQueryable()
                .Select(ProductAttributeProjection.ToProductAttributeResponse())
                .ToList(),
            x.ProductVariants
                .AsQueryable()
                .Select(ProductVariantProjection.ToProductVariantResponse())
                .ToList()
        );
}