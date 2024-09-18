using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Stores.Specifications;
using ECommerce.Inventory.Api.Stores.Responses;

namespace ECommerce.Inventory.Api.Stores.Queries;

public class GetStoreByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IStoreRepository storeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetStoreByIdSpecification<AdminStoreDetailResponse>(id, StoreProjection.ToAdminStoreDetailResponse());

        var store = await storeRepository.FindAsync(spec, cancellationToken);

        if (store is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(store);
    };
}
