using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class BundleProductEntityConfiguration : BaseEntityConfiguration<BundleProduct>
{
    public override void Configure(EntityTypeBuilder<BundleProduct> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(p => p.PromotionBundle)
            .WithMany(c => c.BundleProducts)
            .HasForeignKey(c => c.PromotionBundleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}