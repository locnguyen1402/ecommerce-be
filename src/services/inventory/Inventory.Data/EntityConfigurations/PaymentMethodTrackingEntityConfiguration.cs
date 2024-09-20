using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class PaymentMethodTrackingEntityConfiguration : BaseEntityConfiguration<PaymentMethodTracking>
{
    public override void Configure(EntityTypeBuilder<PaymentMethodTracking> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.OrderId)
            .IsRequired();

        builder
            .Property(t => t.PaymentMethod)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PaymentMethod.UNSPECIFIED}'");

        builder
            .Property(p => p.Value)
            .HasPrecision(19, 2);

        builder
            .HasOne(p => p.Order)
            .WithMany(p => p.PaymentMethodTrackings)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
