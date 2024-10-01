using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Merchants.Specifications;

public class GetMerchantByIdSpecification : Specification<Merchant>
{
    public GetMerchantByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }
}

public class GetMerchantByIdSpecification<TResult> : Specification<Merchant, TResult>
{
    public GetMerchantByIdSpecification
    (
        Guid id,
        Expression<Func<Merchant, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}