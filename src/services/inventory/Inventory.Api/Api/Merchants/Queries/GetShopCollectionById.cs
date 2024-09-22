using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Specifications;
using ECommerce.Inventory.Api.Merchants.Responses;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Merchants.Queries;

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
