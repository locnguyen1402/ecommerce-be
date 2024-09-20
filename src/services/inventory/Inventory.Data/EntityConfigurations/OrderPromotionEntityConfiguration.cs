using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class OrderPromotionEntityConfiguration : BaseEntityConfiguration<OrderPromotion>
{
    public override void Configure(EntityTypeBuilder<OrderPromotion> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PromotionStatus.NEW}'");

        builder
            .Property(t => t.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{OrderPromotionType.UNSPECIFIED}'");

        builder
            .Property(t => t.BundlePromotionDiscountType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{BundlePromotionDiscountType.UNSPECIFIED}'");

        builder.Property(p => p.MinSpend)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder.Property(p => p.MaxQuantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(p => p.Merchant)
            .WithMany(c => c.OrderPromotions)
            .HasForeignKey(c => c.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Conditions)
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'[]'");
    }
}