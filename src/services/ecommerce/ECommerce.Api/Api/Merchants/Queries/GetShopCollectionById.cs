using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Merchants.Specifications;
using ECommerce.Api.Merchants.Responses;
using ECommerce.Api.Categories.Specifications;

namespace ECommerce.Api.Merchants.Queries;

public class GetShopCollectionByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IShopCollectionRepository shopCollectionRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetShopCollectionByIdSpecification<ShopCollectionResponse>(id, ShopCollectionProjection.ToShopCollectionResponse());

        var shopCollection = await shopCollectionRepository.FindAsync(spec, cancellationToken);

        if (shopCollection is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(shopCollection);
    };
}
