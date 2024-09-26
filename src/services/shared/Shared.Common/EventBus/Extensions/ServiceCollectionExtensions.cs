using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Data.IntegrationEvents;
using ECommerce.Shared.Common.EventBus.Services;

using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Common.EventBus.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrationEvents<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddScoped<IIntegrationEventPublisher, IntegrationEventPublisher<TDbContext>>();

        return services;
    }
}
