using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Categories.Specifications;

public class GetShopCollectionByIdSpecification : Specification<ShopCollection>
{
    public GetShopCollectionByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }
}

public class GetShopCollectionByIdSpecification<TResult> : Specification<ShopCollection, TResult>
{
    public GetShopCollectionByIdSpecification
    (
        Guid id,
        Expression<Func<ShopCollection, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}