using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Stores.Responses;
using ECommerce.Inventory.Api.Stores.Specifications;

namespace ECommerce.Inventory.Api.Stores.Queries;

public class GetStoresQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        IStoreRepository storeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetStoresSpecification<StoreResponse>(
            StoreProjection.ToStoreResponse()
            , keyword
            , pagingQuery
            );

        var items = await storeRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
