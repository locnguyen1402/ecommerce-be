using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class PaymentStatusTrackingEntityConfiguration : BaseEntityConfiguration<PaymentStatusTracking>
{
    public override void Configure(EntityTypeBuilder<PaymentStatusTracking> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.OrderId)
            .IsRequired();

        builder
            .Property(t => t.PaymentStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PaymentStatus.UNSPECIFIED}'");

        builder
            .HasOne(p => p.Order)
            .WithMany(p => p.PaymentStatusTrackings)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
