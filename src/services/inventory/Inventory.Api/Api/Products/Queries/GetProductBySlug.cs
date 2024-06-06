using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;

namespace ECommerce.Inventory.Api.Products.Queries;

public class GetProductBySlugQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string slug,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProductBySlugSpecification(slug);

        var product = await productRepository.FindAsync(spec, cancellationToken);

        if (product is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(product);
    };
}
