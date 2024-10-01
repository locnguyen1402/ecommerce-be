using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Products.Specifications;

public class GetProductByIdsSpecification : Specification<Product>
{
    public GetProductByIdsSpecification
    (
        List<Guid> ids
    )
    {
        Builder.Where(p => ids.Contains(p.Id))
            .AsSplitQuery();
    }
}