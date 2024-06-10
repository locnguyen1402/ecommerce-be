using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Categories.Specifications;

public class GetCategoryByIdSpecification : Specification<Category>
{
    public GetCategoryByIdSpecification
    (
        Guid id
    )
    {
        Builder.Where(p => p.Id == id);
    }
}

public class GetCategoryByIdSpecification<TResult> : Specification<Category, TResult>
{
    public GetCategoryByIdSpecification
    (
        Guid id,
        Expression<Func<Category, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}