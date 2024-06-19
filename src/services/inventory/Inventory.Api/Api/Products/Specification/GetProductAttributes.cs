using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Queries;
using System.Linq.Expressions;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductAttributesSpecification : Specification<ProductAttribute>
{
    public GetProductAttributesSpecification
    (
        string? keyword,
        PagingQuery pagingQuery
    )
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword));
        }


        Builder.Paginate(pagingQuery.PageIndex, pagingQuery.PageSize);
    }
}

public class GetProductAttributesSpecification<TResult> : Specification<ProductAttribute, TResult>
{
    public GetProductAttributesSpecification
    (
        string? keyword,
        PagingQuery pagingQuery,
        Expression<Func<ProductAttribute, TResult>> selector
    ) : base(selector)
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword));
        }

        Builder.Paginate(pagingQuery.PageIndex, pagingQuery.PageSize);
    }
}