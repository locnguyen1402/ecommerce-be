using ECommerce.Shared.Libs.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

using ILikeExpression = Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal.PgILikeExpression;

namespace ECommerce.Shared.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureExtensions(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("unaccent");
    }

    public static void UseCustomPostgreSQLDbFunctions(this ModelBuilder builder)
    {
        builder
            .HasDbFunction(() => StringExtensions.ILike(default!, default!))
#pragma warning disable EF1001 // Internal EF Core API usage.
            .HasTranslation(args => new ILikeExpression(
#pragma warning restore EF1001 // Internal EF Core API usage.
                args[0],
                args[1],
                null,
                null
            ));
    }

    public static void UseCustomDbFunctions(this ModelBuilder builder)
    {
        builder
            .HasDbFunction(() => StringExtensions.Like(default!, default!))
            .HasTranslation(args => new LikeExpression(
                args[0],
                args[1],
                null,
                null
            ));
    }
}
