using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class PromotionAddOnDealEntityConfiguration : BaseEntityConfiguration<PromotionAddOnDeal>
{
    public override void Configure(EntityTypeBuilder<PromotionAddOnDeal> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.AddOnDealType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PromotionAddOnDealType.UNSPECIFIED}'");

        builder.Property(p => p.MinSpend)
            .HasDefaultValue(0);

        builder.Property(p => p.MaxGift)
            .HasDefaultValue(0);

        builder.HasOne(p => p.Promotion)
            .WithMany(c => c.PromotionAddOnDeals)
            .HasForeignKey(c => c.PromotionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}