using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record ProductAttributeResponse(
    Guid Id,
    string Name
);

public class ProductAttributeProjection
{
    public static Expression<Func<ProductAttribute, ProductAttributeResponse>> ToProductAttributeResponse()
        => x =>
        new ProductAttributeResponse(
            x.Id,
            x.Name
        );
}