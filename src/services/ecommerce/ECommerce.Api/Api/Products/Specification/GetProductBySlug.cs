using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Products.Specifications;

public class GetProductBySlugSpecification : Specification<Product>
{
    public GetProductBySlugSpecification
    (
        string slug
    )
    {
        Builder.Where(p => p.Slug == slug);
    }
}

public class GetProductBySlugSpecification<TResult> : Specification<Product, TResult>
{
    public GetProductBySlugSpecification
    (
        string slug,
        Expression<Func<Product, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Slug == slug);
    }
}