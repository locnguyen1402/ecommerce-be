namespace ECommerce.Shared.Libs.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        return services.AddAutoMapper(assemblies);
    }
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        services.AddValidatorsFromAssemblies(assemblies);
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection ConfigController(this IServiceCollection services)
    {
        services
            .AddControllers(opt =>
            {
                opt.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            })
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNamingPolicy = JsonConstant.JsonSerializerOptions.PropertyNamingPolicy;
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonConstant.JsonSerializerOptions.DefaultIgnoreCondition;
                opt.JsonSerializerOptions.ReferenceHandler = JsonConstant.JsonSerializerOptions.ReferenceHandler;
                opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonConstant.JsonSerializerOptions.DictionaryKeyPolicy;

                foreach (var converter in JsonConstant.JsonSerializerOptions.Converters)
                    opt.JsonSerializerOptions.Converters.Add(converter);
            });

        return services;
    }

    public static IServiceCollection ConfigDbContext<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(option =>
        {
            option
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }

    public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.UseOneOfForPolymorphism();
            });

        return services;
    }

    private static Assembly[] GetAssemblies()
    {
        var entryAssembly = Assembly.GetEntryAssembly() ?? throw new NullReferenceException("entryAssembly");

        var assemblyNames = entryAssembly
            .GetReferencedAssemblies()
            .Append(entryAssembly.GetName());

        var assemblies = assemblyNames.Select(t =>
        {
            var assembly = Assembly.Load(t);

            return assembly;
        }).Where(t => t != null).ToArray();

        return assemblies;
    }
}