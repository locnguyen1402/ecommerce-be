using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class OrderItemEntityConfiguration : BaseEntityConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ProductName)
            .HasMaxLength(200);

        builder.Property(p => p.ProductDescription)
            .HasMaxLength(100);

        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder
            .Property(p => p.UnitPrice)
            .IsRequired()
            .HasPrecision(19, 2);

        builder
            .Property(p => p.TotalPrice)
            .IsRequired()
            .HasPrecision(19, 2);

        builder
            .Property(p => p.VatPrice)
            .HasPrecision(19, 2);

        builder
            .Property(p => p.VatPercent)
            .HasPrecision(6, 2);

        builder
            .Property(p => p.ListPrice)
            .HasPrecision(19, 2);

        builder
            .Property(p => p.TotalVatPrice)
            .HasPrecision(19, 2);

        builder.HasOne(p => p.Order)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.ProductVariant)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(c => c.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}