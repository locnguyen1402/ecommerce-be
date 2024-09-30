using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Customers.Specifications;

public class GetContactByIdSpecification : Specification<Contact>
{
    public GetContactByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }

    public GetContactByIdSpecification
    (
        Guid id,
        Guid customerId
    )
    {
        Builder.Where(p => p.Id == id && p.CustomerId == customerId);
    }
}

public class GetContactByIdSpecification<TResult> : Specification<Contact, TResult>
{
    public GetContactByIdSpecification
    (
        Guid id,
        Expression<Func<Contact, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}