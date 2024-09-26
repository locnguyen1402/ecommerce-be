using ECommerce.Inventory.Domain.AggregatesModel;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Directories.Responses;
using ECommerce.Inventory.Api.Directories.Specifications;

namespace ECommerce.Inventory.Api.Directories.Queries;

public class AdminGetWardsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        Guid districtId,
        PagingQuery pagingQuery,
        IWardRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new AdminGetWardsSpecification<AdminWardResponse>(
            WardProjection.ToAdminWardResponse()
            , keyword
            , districtId
            , pagingQuery
        );

        var wards = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(wards);
    };
}
