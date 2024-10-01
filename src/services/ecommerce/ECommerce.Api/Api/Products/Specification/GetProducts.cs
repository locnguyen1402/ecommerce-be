using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Products.Specifications;

public class GetProductsSpecification : Specification<Product>
{
    public GetProductsSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery
    )
    {
        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }

        Builder.Where(BuildCriteria(keyword, null));
    }

    public GetProductsSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery,
        Guid? shopCollectionId
    ) : this(keyword, pagingQuery)
    {
        Builder.Where(BuildCriteria(keyword, shopCollectionId));
    }

    public static Expression<Func<Product, bool>> BuildCriteria(
        string? keyword,
        Guid? shopCollectionId
    )
    {
        Expression<Func<Product, bool>> criteria = p => true;

        if (shopCollectionId.HasValue)
            criteria = criteria.And(p => p.ShopCollections.Any(scp => scp.Id == shopCollectionId));

        if (!string.IsNullOrEmpty(keyword))
            criteria = criteria.And(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name), EF.Functions.Unaccent($"%{keyword}%")));

        return criteria;
    }
}

public class GetProductsSpecification<TResult> : Specification<Product, TResult>
{
    public GetProductsSpecification
    (
        Expression<Func<Product, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery
    ) : base(selector)
    {
        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }

        Builder.Where(GetProductsSpecification.BuildCriteria(keyword, null));
    }

    public GetProductsSpecification
    (
        Expression<Func<Product, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery,
        Guid? shopCollectionId
    ) : this(selector, keyword, pagingQuery)
    {
        Builder.Where(GetProductsSpecification.BuildCriteria(keyword, shopCollectionId));
    }
}