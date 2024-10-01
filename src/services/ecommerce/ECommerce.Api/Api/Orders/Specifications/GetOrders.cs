using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Orders.Specifications;

public class GetOrdersSpecification : Specification<Order>
{
    public GetOrdersSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.OrderNumber.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetOrdersSpecification<TResult> : Specification<Order, TResult>
{
    public GetOrdersSpecification
    (
        Expression<Func<Order, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.OrderNumber.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}