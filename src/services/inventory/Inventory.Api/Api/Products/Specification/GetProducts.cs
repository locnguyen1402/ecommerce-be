using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Queries;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductsSpecification : Specification<Product>
{
    public GetProductsSpecification
    (
        PagingQuery query
    )
    {
        Builder.Paginate(query.PageIndex, query.PageSize);
    }
}