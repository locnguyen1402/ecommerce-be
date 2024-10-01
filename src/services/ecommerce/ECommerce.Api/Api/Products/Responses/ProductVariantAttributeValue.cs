using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Products.Responses;

public record ProductVariantAttributeValueResponse(Guid AttributeId, string Value);

public class ProductVariantAttributeValueProjection
{
    public static Expression<Func<ProductVariantAttributeValue, ProductVariantAttributeValueResponse>> ToProductVariantAttributeValueResponse()
        => x => new ProductVariantAttributeValueResponse(x.ProductAttributeId, x.Value);
}