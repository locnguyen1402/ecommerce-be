using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class AddOnDealProductEntityConfiguration : BaseEntityConfiguration<AddOnDealProduct>
{
    public override void Configure(EntityTypeBuilder<AddOnDealProduct> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(p => p.PromotionAddOnDeal)
            .WithMany(c => c.AddOnDealProducts)
            .HasForeignKey(c => c.PromotionAddOnDealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}