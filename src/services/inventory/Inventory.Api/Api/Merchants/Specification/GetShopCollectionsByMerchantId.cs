using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Merchants.Specifications;

public class GetShopCollectionsByMerchantIdSpecification : Specification<ShopCollection>
{
    public GetShopCollectionsByMerchantIdSpecification
    (
        Guid merchantId,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Include(x => x.Merchant);

        Builder.Where(x => x.MerchantId == merchantId);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetShopCollectionsByMerchantIdSpecification<TResult> : Specification<ShopCollection, TResult>
{
    public GetShopCollectionsByMerchantIdSpecification
    (
        Expression<Func<ShopCollection, TResult>> selector,
        Guid merchantId,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Include(x => x.Merchant);

        Builder.Where(x => x.MerchantId == merchantId);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}