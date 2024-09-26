using ECommerce.Inventory.Api.Directories.Queries;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Endpoints;

public class DirectoryEndpoint(WebApplication app) : MinimalEndpoint(app, "/inventory")
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