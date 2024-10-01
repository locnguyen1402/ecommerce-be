using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Categories.Specifications;

public class GetCategoryBySlugSpecification : Specification<Category>
{
    public GetCategoryBySlugSpecification
    (
        string slug
    )
    {
        Builder.Where(p => p.Slug == slug);
    }
}

public class GetCategoryBySlugSpecification<TResult> : Specification<Category, TResult>
{
    public GetCategoryBySlugSpecification
    (
        string slug,
        Expression<Func<Category, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Slug == slug);
    }
}