using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Specification;
using ECommerce.Shared.Common.Queries;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductAttributesSpecification : Specification<ProductAttribute>
{
    public GetProductAttributesSpecification
    (
        string? keyword,
        PagingQuery pagingQuery
    )
    {
        if (!string.IsNullOrEmpty(keyword))
        {
            Builder.Where(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name), EF.Functions.Unaccent($"%{keyword}%")));
        }

        Builder.Paginate(pagingQuery);
    }
}

public class GetProductAttributesSpecification<TResult> : Specification<ProductAttribute, TResult>
{
    public GetProductAttributesSpecification
    (
        Expression<Func<ProductAttribute, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        if (!string.IsNullOrEmpty(keyword))
        {
            Builder.Where(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name), EF.Functions.Unaccent($"%{keyword}%")));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}