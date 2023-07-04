using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Shared.Common;
public abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : Entity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()")
            .IsRequired();
    }
}