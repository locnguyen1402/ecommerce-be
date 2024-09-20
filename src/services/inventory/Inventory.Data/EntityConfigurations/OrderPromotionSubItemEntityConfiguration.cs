using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class OrderPromotionSubItemEntityConfiguration : BaseEntityConfiguration<OrderPromotionSubItem>
{
    public override void Configure(EntityTypeBuilder<OrderPromotionSubItem> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{OrderPromotionSubItemType.UNSPECIFIED}'");

        builder.Property(p => p.DiscountPrice)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder.Property(p => p.DiscountPercentage)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder
            .Property(t => t.NoProductsPerOrderLimit)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{NoProductsPerOrderLimit.SPECIFIC}'");

        builder.Property(p => p.MaxItemsPerOrder)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasOne(p => p.OrderPromotion)
            .WithMany(c => c.SubItems)
            .HasForeignKey(c => c.OrderPromotionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany(c => c.OrderPromotionSubItems)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}