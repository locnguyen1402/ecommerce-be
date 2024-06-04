using System.Reflection;
using Microsoft.AspNetCore.Builder;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Shared.Common.Extensions;
public static class EndpointExtensions
{
    public static IApplicationBuilder AutoMapEndpoints(
        this WebApplication app,
        Assembly assembly)
    {
        assembly
            .GetTypes()
            .Where(type => type.GetInterface(nameof(IMinimalEndpoint)) != null)
            .ToList()
            .ForEach(type => Activator.CreateInstance(type, app));

        return app;
    }
}