using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Orders.Specifications;

public class GetOrderByOrderNumberSpecification : Specification<Order>
{
    public GetOrderByOrderNumberSpecification
    (
        string orderNumber
    )
    {
        Builder.Where(p => p.OrderNumber == orderNumber);
    }
}

public class GetOrderByOrderNumberSpecification<TResult> : Specification<Order, TResult>
{
    public GetOrderByOrderNumberSpecification
    (
        string orderNumber,
        Expression<Func<Order, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.OrderNumber == orderNumber);
    }
}