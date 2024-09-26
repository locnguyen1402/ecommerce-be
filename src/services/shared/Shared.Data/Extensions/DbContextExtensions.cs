using Microsoft.EntityFrameworkCore;

using Npgsql;

using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Data.Interceptors;

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

        options.ConfigureCommonSettings();

        return options;
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

    public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder options, NpgsqlDataSource dataSource)
    {
        options.UseNpgsql(
            dataSource,
            sqlOptions =>
            {
                sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(SchemaConstants.COMMAND_TIMEOUT).TotalSeconds);
                sqlOptions.MigrationsHistoryTable(DatabaseSchemaConstants.MIGRATIONS_TABLE);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: SchemaConstants.MAX_RETRY_COUNT, maxRetryDelay: TimeSpan.FromSeconds(SchemaConstants.COMMAND_TIMEOUT), errorCodesToAdd: null);
            });

        options.ConfigureCommonSettings();

        options.AddInterceptors(new SaveChangesInterceptor());

        return options;
    }
}
