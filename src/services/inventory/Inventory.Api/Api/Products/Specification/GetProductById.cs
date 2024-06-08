using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductByIdSpecification : Specification<Product>
{
    public GetProductByIdSpecification
    (
        Guid id
    )
    {
        // Builder.Include(p => p.ProductVariants);
        // Builder.Include("ProductVariants.ProductVariantAttributeValues");

        Builder.Where(p => p.Id == id);
    }
}

public class GetProductByIdSpecification<TResult> : Specification<Product, TResult>
{
    public GetProductByIdSpecification
    (
        Guid id,
        Expression<Func<Product, TResult>> selector
    ) : base(selector)
    {
        Builder.Where(p => p.Id == id);
    }
}