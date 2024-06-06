using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductByIdSpecification : Specification<Product>
{
    public GetProductByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }
}