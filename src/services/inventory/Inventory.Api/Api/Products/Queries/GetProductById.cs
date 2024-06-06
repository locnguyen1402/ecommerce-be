using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;

namespace ECommerce.Inventory.Api.Products.Queries;

public class GetProductByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProductByIdSpecification(id);

        var product = await productRepository.FindAsync(id, cancellationToken);
        // var product = await productRepository.FindAsync(spec, cancellationToken);

        if (product is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(product);
    };
}
