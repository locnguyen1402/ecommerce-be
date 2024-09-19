using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class AddOnDealGiftEntityConfiguration : BaseEntityConfiguration<AddOnDealGift>
{
    public override void Configure(EntityTypeBuilder<AddOnDealGift> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.PromotionAddOnDeal)
            .WithMany(c => c.AddOnDealGifts)
            .HasForeignKey(c => c.PromotionAddOnDealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}