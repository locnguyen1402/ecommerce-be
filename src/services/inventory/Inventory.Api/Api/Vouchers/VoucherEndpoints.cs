using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Vouchers.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class VoucherEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/vouchers")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Vouchers");
    }
    public override void MapEndpoints()
    {
        Builder.MapPost<CreateVoucherCommandHandler>("/");
    }
}