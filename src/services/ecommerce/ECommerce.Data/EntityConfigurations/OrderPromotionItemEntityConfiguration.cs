using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class OrderPromotionItemEntityConfiguration : BaseEntityConfiguration<OrderPromotionItem>
{
    public override void Configure(EntityTypeBuilder<OrderPromotionItem> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.OrderPromotion)
            .WithMany(c => c.Items)
            .HasForeignKey(c => c.OrderPromotionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany(c => c.OrderPromotionItems)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}