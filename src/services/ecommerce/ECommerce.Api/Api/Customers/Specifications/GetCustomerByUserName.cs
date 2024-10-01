using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Customers.Specifications;

public class GetCustomerByUserNameSpecification : Specification<Customer>
{
    public GetCustomerByUserNameSpecification
    (
        string userName
    )
    {
        Builder.Where(x => x.UserName != null && x.UserName == userName);
    }
}

public class GetCustomerByUserNameSpecification<TResult> : Specification<Customer, TResult>
{
    public GetCustomerByUserNameSpecification
    (
        Expression<Func<Customer, TResult>> selector,
        string userName
    ) : base(selector)
    {

        Builder.Where(x => x.UserName != null && x.UserName == userName);
    }
}