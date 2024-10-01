using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Vouchers.Commands;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class VoucherEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/vouchers")
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