using ECommerce.Shared.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Shared.Common.Extensions;

public static class EntityConfigurationExtensions
{
    public static void MapId<TEntity>(this EntityTypeBuilder<TEntity> builder, string defaultValue) where TEntity : class, IEntity<Guid>
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasDefaultValueSql(defaultValue);
    }
}
