using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Categories.Specifications;

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
            Builder.Paginate(pagingQuery);
        }
    }
}