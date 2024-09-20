using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Orders.Specifications;

public class GetOrderByIdSpecification : Specification<Order>
{
    public GetOrderByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }
}

public class GetOrderByIdSpecification<TResult> : Specification<Order, TResult>
{
    public GetOrderByIdSpecification
    (
        Guid id,
        Expression<Func<Order, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}