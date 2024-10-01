using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Discounts.Specifications;

public class GetDiscountsSpecification : Specification<Discount>
{
    public GetDiscountsSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetDiscountsSpecification<TResult> : Specification<Discount, TResult>
{
    public GetDiscountsSpecification
    (
        Expression<Func<Discount, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}