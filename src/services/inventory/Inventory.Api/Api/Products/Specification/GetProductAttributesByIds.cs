using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductAttributesByIdsSpecification : Specification<ProductAttribute>
{
    public GetProductAttributesByIdsSpecification
    (
        List<Guid> ids,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(x => ids.Contains(x.Id));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}