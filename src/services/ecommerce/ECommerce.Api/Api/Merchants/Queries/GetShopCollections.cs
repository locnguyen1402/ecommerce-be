using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Merchants.Responses;
using ECommerce.Api.Merchants.Specifications;

namespace ECommerce.Api.Merchants.Queries;

public class GetShopCollectionsQueryHandler : IEndpointHandler
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
        var spec = new GetShopCollectionsSpecification<ShopCollectionResponse>(
            ShopCollectionProjection.ToShopCollectionResponse()
            , keyword
            , hasChildren
            , pagingQuery
            );

        var items = await shopCollectionRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
