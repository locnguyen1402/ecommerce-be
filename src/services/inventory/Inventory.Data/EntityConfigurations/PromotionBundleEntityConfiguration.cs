using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class PromotionBundleEntityConfiguration : BaseEntityConfiguration<PromotionBundle>
{
    public override void Configure(EntityTypeBuilder<PromotionBundle> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.BundleType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PromotionBundleType.UNSPECIFIED}'");

        builder.HasOne(p => p.Promotion)
            .WithMany(c => c.PromotionBundles)
            .HasForeignKey(c => c.PromotionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}