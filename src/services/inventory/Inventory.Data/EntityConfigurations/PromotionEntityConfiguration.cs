using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class PromotionEntityConfiguration : BaseEntityConfiguration<Promotion>
{
    public override void Configure(EntityTypeBuilder<Promotion> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(i => i.Name);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasIndex(i => i.Slug)
            .IsUnique();

        builder
            .Property(t => t.PromotionType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PromotionType.UNSPECIFIED}'");

        builder
            .Property(t => t.PromotionStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PromotionStatus.UNSPECIFIED}'");

        builder.HasOne(p => p.Merchant)
            .WithMany(c => c.Promotions)
            .HasForeignKey(c => c.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}