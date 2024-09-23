using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

using ECommerce.Shared.Common.Extensions;

namespace ECommerce.Shared.Data.EntityConfigurations;

public abstract class EntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : class, IEntity<Guid>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.MapId(DatabaseSchemaConstants.DEFAULT_UUID_GENERATOR);

        var interfaces = builder.Metadata.ClrType.GetInterfaces();

        if (interfaces.Any(p => p == typeof(ICreationAuditing)))
            builder.MapCreationAuditing(DatabaseSchemaConstants.DEFAULT_UTC_TIMESTAMP);

        if (interfaces.Any(p => p == typeof(IUpdateAuditing)))
            builder.MapUpdateAuditing();

        if (interfaces.Any(x => x == typeof(IExtraProperties<>)))
            builder.Property("ExtraProperties")
                .HasColumnType("jsonb");
    }
}
