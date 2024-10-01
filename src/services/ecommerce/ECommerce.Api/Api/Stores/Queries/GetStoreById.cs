using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Stores.Specifications;
using ECommerce.Api.Stores.Responses;

namespace ECommerce.Api.Stores.Queries;

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
