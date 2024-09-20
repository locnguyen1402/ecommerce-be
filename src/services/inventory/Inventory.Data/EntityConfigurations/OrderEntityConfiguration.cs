using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class OrderEntityConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.CustomerId)
            .IsRequired();

        builder.HasIndex(i => i.CustomerId);

        builder.Property(p => p.StoreId)
            .IsRequired();

        builder.HasIndex(i => i.StoreId);

        builder.Property(p => p.OrderNumber)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(i => i.OrderNumber)
            .IsUnique();

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{OrderStatus.TO_PAY}'");

        builder
            .Property(t => t.PaymentStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PaymentStatus.UNPAID}'");

        builder
            .Property(t => t.PaymentMethod)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{PaymentMethod.UNSPECIFIED}'");

        builder
            .Property(p => p.TotalPrice)
            .IsRequired()
            .HasPrecision(19, 2);

        builder
            .Property(p => p.TotalDiscountPrice)
            .HasPrecision(19, 2);

        builder
            .Property(p => p.TotalItemPrice)
            .HasPrecision(19, 2);

        builder
            .Property(p => p.DeliveryFee)
            .HasPrecision(19, 2);

        builder
            .Property(p => p.TotalVatPrice)
            .HasPrecision(19, 2);

        builder
            .Property(p => p.TotalExceptVatPrice)
            .HasPrecision(19, 2);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);

        builder.HasOne(p => p.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Store)
            .WithMany(c => c.Orders)
            .HasForeignKey(c => c.StoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}