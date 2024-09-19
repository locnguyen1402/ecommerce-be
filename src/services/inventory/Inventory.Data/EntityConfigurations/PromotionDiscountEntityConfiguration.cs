using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class PromotionDiscountEntityConfiguration : BaseEntityConfiguration<PromotionDiscount>
{
    public override void Configure(EntityTypeBuilder<PromotionDiscount> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(p => p.Promotion)
            .WithMany(c => c.PromotionDiscounts)
            .HasForeignKey(c => c.PromotionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}