using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Categories.Specifications;

public class GetCategoriesSpecification : Specification<Category>
{
    public GetCategoriesSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetCategoriesSpecification<TResult> : Specification<Category, TResult>
{
    public GetCategoriesSpecification
    (
        Expression<Func<Category, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword) && x.ParentId == null);
        }
        else
        {
            Builder.Where(x => x.ParentId == null);
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}
