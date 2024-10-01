using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Products.Responses;

public record ProductAttributeResponse(
    Guid Id,
    string Name
);

public record ProductAttributeWithValuesResponse(
    Guid Id,
    string Name,
    HashSet<string> Values
);

public static class ProductAttributeProjection
{
    public static ProductAttributeResponse ToProductAttributeResponse(this ProductAttribute attribute)
    {
        return ToProductAttributeResponse().Compile().Invoke(attribute);
    }

    public static Expression<Func<ProductAttribute, ProductAttributeResponse>> ToProductAttributeResponse()
        => x =>
        new ProductAttributeResponse(
            x.Id,
            x.Name
        );
}