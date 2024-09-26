using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class FilterProductVariantsSpecification : Specification<ProductVariant>
{
    public FilterProductVariantsSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(BuildCriteria(keyword, null, null));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }

    public static Expression<Func<ProductVariant, bool>> BuildCriteria
    (
        string? keyword
        , List<Guid>? shopCollectionIds
        , List<Guid>? notInShopCollectionIds
    )
    {
        Expression<Func<ProductVariant, bool>> criteria = p => true;

        if (!string.IsNullOrEmpty(keyword))
            criteria = criteria.And(p => p.Product != null && EF.Functions.ILike(EF.Functions.Unaccent(p.Product.Name), EF.Functions.Unaccent($"%{keyword}%")));

        if (shopCollectionIds != null && shopCollectionIds.Count > 0)
            criteria = criteria.And(p => p.Product != null && p.Product.ShopCollectionProducts.Any(sc => shopCollectionIds.Contains(sc.ShopCollectionId)));

        if (notInShopCollectionIds != null && notInShopCollectionIds.Count > 0)
            criteria = criteria.And(p => p.Product != null && !p.Product.ShopCollectionProducts.Any(sc => notInShopCollectionIds.Contains(sc.ShopCollectionId)));

        return criteria;
    }

}

public class FilterProductVariantsSpecification<TResult> : Specification<ProductVariant, TResult>
{
    public FilterProductVariantsSpecification
    (
        Expression<Func<ProductVariant, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(FilterProductVariantsSpecification.BuildCriteria(keyword, null, null));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }

    public FilterProductVariantsSpecification
    (
        Expression<Func<ProductVariant, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery,
        List<Guid>? shopCollectionIds,
        List<Guid>? notInShopCollectionIds
    ) : this(selector, keyword, pagingQuery)
    {
        Builder.Where(FilterProductVariantsSpecification.BuildCriteria(keyword, shopCollectionIds, notInShopCollectionIds));
    }
}