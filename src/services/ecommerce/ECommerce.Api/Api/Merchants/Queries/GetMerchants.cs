using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Merchants.Responses;
using ECommerce.Api.Merchants.Specifications;

namespace ECommerce.Api.Merchants.Queries;

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
