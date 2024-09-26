using ECommerce.Inventory.Domain.AggregatesModel;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Directories.Responses;
using ECommerce.Inventory.Api.Directories.Specifications;

namespace ECommerce.Inventory.Api.Directories.Queries;

public class GetDistrictsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        Guid provinceId,
        PagingQuery pagingQuery,
        IDistrictRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetDistrictsSpecification<DistrictResponse>(
            DistrictProjection.ToDistrictResponse()
            , keyword
            , provinceId
            , pagingQuery
        );

        var districts = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(districts);
    };
}
