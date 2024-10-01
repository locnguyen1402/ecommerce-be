using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class ProductPromotionItemEntityConfiguration : BaseEntityConfiguration<ProductPromotionItem>
{
    public override void Configure(EntityTypeBuilder<ProductPromotionItem> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.ListPrice)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder
            .Property(p => p.DiscountPrice)
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder
            .Property(p => p.DiscountPercentage)
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder
            .Property(p => p.Quantity)
            .HasDefaultValue(0);

        builder
            .Property(p => p.MaxItemsPerOrder)
            .HasDefaultValue(0);

        builder.HasOne(p => p.ProductPromotion)
            .WithMany(c => c.Items)
            .HasForeignKey(c => c.ProductPromotionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany(c => c.ProductPromotionItems)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.ProductVariant)
            .WithMany(c => c.ProductPromotionItems)
            .HasForeignKey(c => c.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}