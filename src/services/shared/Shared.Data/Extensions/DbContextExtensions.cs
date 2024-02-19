using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Data.Extensions;

public static class DbContextExtensions
{
    public static DbContextOptionsBuilder UseMigrationDatabase<TMigrationsAssembly>(this DbContextOptionsBuilder options, string connectionString)
    {
        options.UseNpgsql(
            connectionString,
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(TMigrationsAssembly).Assembly.GetName().Name);
                // sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(SchemaConstants.COMMAND_TIMEOUT).TotalSeconds);
                // sqlOptions.EnableRetryOnFailure(maxRetryCount: SchemaConstants.MAX_RETRY_COUNT, maxRetryDelay: TimeSpan.FromSeconds(SchemaConstants.COMMAND_TIMEOUT), errorCodesToAdd: null);
            });

        return options.ConfigureCommonSettings();
    }

    private static DbContextOptionsBuilder ConfigureCommonSettings(this DbContextOptionsBuilder options)
    {
        options.UseSnakeCaseNamingConvention();

        // #if DEBUG
        //     options.EnableSensitiveDataLogging();
        // #endif

        // options.ReplaceService<IHistoryRepository, CustomHistoryRepository>();

        return options;
    }
}
