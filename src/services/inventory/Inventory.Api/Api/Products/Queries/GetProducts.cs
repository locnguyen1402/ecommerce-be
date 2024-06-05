using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Queries;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

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

        return await productRepository.PaginateAsync(spec, cancellationToken);
    };
}
