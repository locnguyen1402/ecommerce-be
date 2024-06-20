using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Categories.Specifications;

public class GetCategoriesByIdsSpecification : Specification<Category>
{
    public GetCategoriesByIdsSpecification
    (
        List<Guid> ids,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(x => ids.Contains(x.Id));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery.PageIndex, pagingQuery.PageSize);
        }
    }
}