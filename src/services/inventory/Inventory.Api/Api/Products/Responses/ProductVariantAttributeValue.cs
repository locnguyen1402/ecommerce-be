using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record ProductVariantAttributeValueResponse(Guid AttributeId, string Value);

public class ProductVariantAttributeValueProjection
{
    public static Expression<Func<ProductVariantAttributeValue, ProductVariantAttributeValueResponse>> FromProductVariantAttributeValue()
        => x => new ProductVariantAttributeValueResponse(x.ProductAttributeId, x.Value);
}