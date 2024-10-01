using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Customers.Specifications;

public class GetCustomerByIdSpecification : Specification<Customer>
{
    public GetCustomerByIdSpecification
    (
        Guid id
    )
    {
        Builder
            .Include(p => p.Contacts)
            .Where(p => p.Id == id);
    }
}

public class GetCustomerByIdSpecification<TResult> : Specification<Customer, TResult>
{
    public GetCustomerByIdSpecification
    (
        Guid id,
        Expression<Func<Customer, TResult>> selector
    ) : base(selector)
    {
        Builder
            .Include(p => p.Contacts)
            .Where(p => p.Id == id);
    }
}