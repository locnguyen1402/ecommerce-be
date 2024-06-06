using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;

namespace ECommerce.Inventory.Api.Products.Queries;

public class GetProductsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        PagingQuery pagingQuery,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProductsSpecification(pagingQuery: pagingQuery);

        var products = await productRepository.PaginateAsync(spec, cancellationToken);

        products.PopulatePaginationInfo();

        return TypedResults.Ok(products);
    };
}
