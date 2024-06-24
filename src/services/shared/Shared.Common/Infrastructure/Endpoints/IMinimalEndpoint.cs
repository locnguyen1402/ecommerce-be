using Microsoft.AspNetCore.Routing;

namespace ECommerce.Shared.Common.Infrastructure.Endpoint;

public interface IMinimalEndpoint
{
    void Configure(RouteGroupBuilder builder);
    void MapEndpoints();
}