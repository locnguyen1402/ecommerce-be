using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record ProductVariantResponse(
    Guid Id,
    int Stock,
    decimal Price,
    List<ProductVariantAttributeValueResponse> Values
);

public class ProductVariantProjection
{
    public static Expression<Func<ProductVariant, ProductVariantResponse>> ToProductVariantResponse()
        => x =>
        new ProductVariantResponse(
            x.Id,
            x.Stock, 
            x.Price,
            x.ProductVariantAttributeValues
                .AsQueryable()
                .Select(ProductVariantAttributeValueProjection.ToProductVariantAttributeValueResponse())
                .ToList()
        );
}