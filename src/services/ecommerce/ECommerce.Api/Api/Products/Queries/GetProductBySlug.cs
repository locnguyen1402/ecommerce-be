using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Products.Specifications;
using ECommerce.Api.Products.Responses;

namespace ECommerce.Api.Products.Queries;

public class GetProductBySlugQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string slug,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProductBySlugSpecification<AdminProductDetailResponse>(slug, ProductProjection.ToAdminProductDetailResponse());

        var product = await productRepository.FindAsync(spec, cancellationToken);

        if (product is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(product);
    };
}
