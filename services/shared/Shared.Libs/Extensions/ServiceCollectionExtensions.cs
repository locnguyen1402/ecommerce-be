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

    public static IMvcBuilder ConfigController(this IServiceCollection services)
    {
        return services.AddControllers(opt =>
        {
            opt.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });
    }

    public static IServiceCollection ConfigureJson(this IServiceCollection services)
    {
        services
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonConstant.JsonSerializerOptions.PropertyNamingPolicy;
                options.SerializerOptions.DefaultIgnoreCondition = JsonConstant.JsonSerializerOptions.DefaultIgnoreCondition;
                options.SerializerOptions.ReferenceHandler = JsonConstant.JsonSerializerOptions.ReferenceHandler;
                options.SerializerOptions.DictionaryKeyPolicy = JsonConstant.JsonSerializerOptions.DictionaryKeyPolicy;

                foreach (var converter in JsonConstant.JsonSerializerOptions.Converters)
                    options.SerializerOptions.Converters.Add(converter);
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