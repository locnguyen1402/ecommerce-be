using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Merchants.Specifications;

public class GetShopCollectionsSpecification : Specification<ShopCollection>
{
    public GetShopCollectionsSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetShopCollectionsSpecification<TResult> : Specification<ShopCollection, TResult>
{
    public GetShopCollectionsSpecification
    (
        Expression<Func<ShopCollection, TResult>> selector,
        string? keyword,
        bool? hasChildren,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(BuildCriteria(keyword, hasChildren));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }


    private static Expression<Func<ShopCollection, bool>> BuildCriteria(string? keyword, bool? hasChildren)
    {
        Expression<Func<ShopCollection, bool>> criteria = p => true;

        if (!string.IsNullOrEmpty(keyword))
            criteria = criteria.And(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name), EF.Functions.Unaccent($"%{keyword}%")));


        if (hasChildren.HasValue)
        {
            if (hasChildren.Value)
                criteria = criteria.And(p => p.ShopCollections.Count == 0);
            else
                criteria = criteria.And(p => p.ShopCollections.Count != 0);
        }

        return criteria;
    }
}