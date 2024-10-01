using ECommerce.Domain.AggregatesModel;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Directories.Responses;
using ECommerce.Api.Directories.Specifications;

namespace ECommerce.Api.Directories.Queries;

public class GetProvincesQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        IProvinceRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProvincesSpecification<ProvinceResponse>(
            ProvinceProjection.ToProvinceResponse()
            , keyword
            , pagingQuery
        );

        var provinces = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(provinces);
    };
}
