using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class FilterProductsSpecification : Specification<Product>
{
    public FilterProductsSpecification
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

    public static Expression<Func<Product, bool>> BuildCriteria
    (
        string? keyword
        , List<Guid>? shopCollectionIds
        , List<Guid>? notInShopCollectionIds
    )
    {
        Expression<Func<Product, bool>> criteria = p => true;

        if (!string.IsNullOrEmpty(keyword))
            criteria = criteria.And(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name), EF.Functions.Unaccent($"%{keyword}%")));

        if (shopCollectionIds != null && shopCollectionIds.Count > 0)
            criteria = criteria.And(p => p.ShopCollectionProducts.Any(sc => shopCollectionIds.Contains(sc.ShopCollectionId)));

        if (notInShopCollectionIds != null && notInShopCollectionIds.Count > 0)
            criteria = criteria.And(p => !p.ShopCollectionProducts.Any(sc => notInShopCollectionIds.Contains(sc.ShopCollectionId)));

        return criteria;
    }

}

public class FilterProductsSpecification<TResult> : Specification<Product, TResult>
{
    public FilterProductsSpecification
    (
        Expression<Func<Product, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(FilterProductsSpecification.BuildCriteria(keyword, null, null));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }

    public FilterProductsSpecification
    (
        Expression<Func<Product, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery,
        List<Guid>? shopCollectionIds,
        List<Guid>? notInShopCollectionIds
    ) : this(selector, keyword, pagingQuery)
    {
        Builder.Where(FilterProductsSpecification.BuildCriteria(keyword, shopCollectionIds, notInShopCollectionIds));
    }
}