using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class OrderStatusTrackingEntityConfiguration : BaseEntityConfiguration<OrderStatusTracking>
{
    public override void Configure(EntityTypeBuilder<OrderStatusTracking> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.OrderId)
            .IsRequired();

        builder
            .Property(t => t.OrderStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{OrderStatus.UNSPECIFIED}'");

        builder.HasOne(p => p.Order)
            .WithMany(c => c.OrderStatusTrackings)
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
