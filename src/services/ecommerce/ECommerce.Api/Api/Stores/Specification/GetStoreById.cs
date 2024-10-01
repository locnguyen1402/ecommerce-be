using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Stores.Specifications;

public class GetStoreByIdSpecification : Specification<Store>
{
    public GetStoreByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }
}

public class GetStoreByIdSpecification<TResult> : Specification<Store, TResult>
{
    public GetStoreByIdSpecification
    (
        Guid id,
        Expression<Func<Store, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}