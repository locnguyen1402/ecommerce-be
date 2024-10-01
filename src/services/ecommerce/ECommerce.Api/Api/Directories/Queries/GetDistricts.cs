using ECommerce.Domain.AggregatesModel;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Directories.Responses;
using ECommerce.Api.Directories.Specifications;

namespace ECommerce.Api.Directories.Queries;

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
