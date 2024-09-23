using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity<Guid>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // builder.HasKey(e => e.Id);

        // builder.Property(e => e.Id)
        //     .HasDefaultValueSql("gen_random_uuid()");

        // builder.Property(e => e.CreatedAt)
        //     .HasDefaultValueSql("now()")
        //     .IsRequired();
    }
}