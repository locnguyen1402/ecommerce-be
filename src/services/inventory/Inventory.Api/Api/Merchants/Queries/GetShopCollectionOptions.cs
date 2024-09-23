using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Specifications;
using ECommerce.Inventory.Api.Merchants.Responses;

namespace ECommerce.Inventory.Api.Merchants.Queries;

public class GetShopCollectionOptionsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        bool? hasChildren,
        PagingQuery pagingQuery,
        IShopCollectionRepository shopCollectionRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetShopCollectionsSpecification<ShopCollectionOption>(
            ShopCollectionProjection.ToShopCollectionOption()
            , keyword
            , hasChildren
            , pagingQuery
            );

        var items = await shopCollectionRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
