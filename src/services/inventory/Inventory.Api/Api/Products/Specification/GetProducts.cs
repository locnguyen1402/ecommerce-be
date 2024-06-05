using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Queries;
using System.Linq.Expressions;

namespace ECommerce.Inventory.Api.Products.Specifications;

public class GetProductsSpecification : Specification<Product>
{
    public GetProductsSpecification
    (
        Expression<Func<Product, bool>>? criteria = null,
        PagingQuery? pagingQuery = null
    )
    {
        if (criteria != null)
        {
            Builder.Where(criteria);
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery.PageIndex, pagingQuery.PageSize);
        }
    }
}