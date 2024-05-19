using Microsoft.AspNetCore.Builder;

namespace ECommerce.Shared.Common.Endpoint;

public interface IMinimalEndpoint
{
    void MapEndpoints();
}