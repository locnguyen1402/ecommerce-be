using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class BundleConditionEntityConfiguration : BaseEntityConfiguration<BundleCondition>
{
    public override void Configure(EntityTypeBuilder<BundleCondition> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.DiscountValue)
            .HasDefaultValue(0);

        builder
            .Property(t => t.DiscountUnit)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{DiscountUnit.UNSPECIFIED}'");

        builder.HasOne(p => p.PromotionBundle)
            .WithMany(c => c.BundleConditions)
            .HasForeignKey(c => c.PromotionBundleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}