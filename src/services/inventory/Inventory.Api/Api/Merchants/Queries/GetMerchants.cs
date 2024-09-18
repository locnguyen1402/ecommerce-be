using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Responses;
using ECommerce.Inventory.Api.Merchants.Specifications;

namespace ECommerce.Inventory.Api.Merchants.Queries;

public class GetMerchantsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        bool? hasStores,
        PagingQuery pagingQuery,
        IMerchantRepository merchantRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetMerchantsSpecification<MerchantResponse>(
            MerchantProjection.ToMerchantResponse()
            , keyword
            , hasStores
            , pagingQuery
            );

        var items = await merchantRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
