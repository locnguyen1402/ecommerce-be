using ECommerce.Shared.Common.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ECommerce.Shared.Common.Extensions;

public static partial class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.TryAddTransient<IEventPublisher>((sp) =>
        {
            var scope = sp.CreateScope();

            return new EventPublisher(scope.ServiceProvider);
        });

        return services;
    }
}
