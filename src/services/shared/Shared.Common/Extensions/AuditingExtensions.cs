using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Shared.Common.Extensions;

public static class AuditingExtensions
{
    public static void MapCreationAuditing<TEntity>(this EntityTypeBuilder<TEntity> builder, string defaultCreatedAt) where TEntity : class, IEntity
    {
        builder.Property(p => ((ICreationAuditing)p).CreatedBy)
            .HasConversion<Guid?>();

        builder.Property(nameof(ICreationAuditing.CreatedAt))
            .IsRequired()
            .HasDefaultValueSql(defaultCreatedAt);
    }

    public static void MapUpdateAuditing<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
    {
        builder.Property(p => ((IUpdateAuditing)p).UpdatedBy)
            .HasConversion<Guid?>();
    }

    public static void MapDeletionAuditing(this EntityTypeBuilder<IDeletionAuditing> builder)
    {
        builder.HasQueryFilter(t => EF.Property<DateTimeOffset?>(t, nameof(IDeletionAuditing.DeletedAt)) == null);
    }
}
