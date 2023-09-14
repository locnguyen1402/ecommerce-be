

namespace ECommerce.Shared.Integration.Extensions;
public static class RestClientsExtensions
{
    public static IServiceCollection RegisterOLRestClient(this IServiceCollection services, string baseUrl)
    {

        services.AddScoped<IWorkRestClient>(serviceProvider =>
        {
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            return new WorkRestClient(baseUrl, mapper);
        });

        services.AddScoped<IBookRestClient>(serviceProvider =>
        {
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            return new BookRestClient(baseUrl, mapper);
        });

        return services;
    }
}