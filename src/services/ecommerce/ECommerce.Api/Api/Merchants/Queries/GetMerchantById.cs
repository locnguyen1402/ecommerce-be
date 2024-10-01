using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Merchants.Specifications;
using ECommerce.Api.Merchants.Responses;

namespace ECommerce.Api.Merchants.Queries;

public class GetMerchantByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IMerchantRepository merchantRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetMerchantByIdSpecification<AdminMerchantDetailResponse>(id, MerchantProjection.ToAdminMerchantDetailResponse());

        var merchant = await merchantRepository.FindAsync(spec, cancellationToken);

        if (merchant is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(merchant);
    };
}
