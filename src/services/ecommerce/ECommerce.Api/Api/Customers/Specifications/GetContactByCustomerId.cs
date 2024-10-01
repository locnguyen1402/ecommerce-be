using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Customers.Specifications;

public class GetContactsByCustomerIdSpecification : Specification<Contact>
{
    public GetContactsByCustomerIdSpecification
    (
        Guid customerId,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(x => x.CustomerId == customerId);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetContactsByCustomerIdSpecification<TResult> : Specification<Contact, TResult>
{
    public GetContactsByCustomerIdSpecification
    (
        Expression<Func<Contact, TResult>> selector,
        Guid customerId,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {

        Builder.Where(x => x.CustomerId == customerId);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}