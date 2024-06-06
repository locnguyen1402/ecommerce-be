using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductBySlugSpecification : Specification<Product>
{
    public GetProductBySlugSpecification
    (
        string slug
    )
    {
        Builder.Where(p => p.Slug == slug);
    }
}