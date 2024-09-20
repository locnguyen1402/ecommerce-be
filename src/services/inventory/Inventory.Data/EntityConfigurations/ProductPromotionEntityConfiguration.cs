using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ProductPromotionEntityConfiguration : BaseEntityConfiguration<ProductPromotion>
{
    public override void Configure(EntityTypeBuilder<ProductPromotion> builder)
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
            .HasDefaultValueSql($"'{ProductPromotionType.UNSPECIFIED}'");

        builder.HasOne(p => p.Merchant)
            .WithMany(c => c.ProductPromotions)
            .HasForeignKey(c => c.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}