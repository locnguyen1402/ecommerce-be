using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;
using ECommerce.Shared.Common.Queries;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Categories.Specifications;

public class GetCategoriesSpecification : Specification<Category>
{
    public GetCategoriesSpecification
    (
        Expression<Func<Category, bool>>? criteria = null,
        PagingQuery? pagingQuery = null
    )
    {
        if (criteria != null)
        {
            Builder.Where(criteria);
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery.PageIndex, pagingQuery.PageSize);
        }
    }
}
