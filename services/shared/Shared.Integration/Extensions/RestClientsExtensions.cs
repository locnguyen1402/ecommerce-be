

namespace ECommerce.Shared.Integration.Extensions;
public static class RestClientsExtensions
{
    public static IServiceCollection RegisterBookRestClient(this IServiceCollection services, string baseUrl)
    {

        services.AddScoped<IWorkRestClient>(serviceProvider =>
        {
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            return new WorkRestClient(baseUrl, mapper);
        });

        return services;
    }
}