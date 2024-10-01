using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class StoreEntityConfiguration : BaseEntityConfiguration<Store>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(t => t.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(t => t.Description)
            .HasMaxLength(500)
            .HasDefaultValueSql("''");

        builder.HasOne(p => p.Merchant)
            .WithMany(c => c.Stores)
            .HasForeignKey(c => c.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}