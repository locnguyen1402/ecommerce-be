using ECommerce.Api.Directories.Queries;
using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Api.Endpoints;

public class DirectoryEndpoint(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Directories");
    }
    public override void MapEndpoints()
    {
        var clientGroup = Builder.MapGroup("/directories");
        var adminGroup = Builder.MapGroup("/admin/directories");

        clientGroup.MapGet<GetProvincesQueryHandler>("/provinces");
        clientGroup.MapGet<GetDistrictsQueryHandler>("/provinces/{provinceId:Guid}/districts/");
        clientGroup.MapGet<GetWardsQueryHandler>("/provinces/districts/{districtId:Guid}/wards");

        adminGroup.MapGet<AdminGetDistrictsQueryHandler>("/provinces/{provinceId:Guid}/districts");
        adminGroup.MapGet<AdminGetWardsQueryHandler>("/provinces/districts/{districtId:Guid}/wards");
    }
}