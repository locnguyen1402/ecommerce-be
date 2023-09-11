using ECommerce.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Orders;
public class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.TotalQuantity)
            .HasDefaultValue(0);

        builder.Property(p => p.TotalPrice)
            .HasDefaultValue(0);

        builder.Property(p => p.OrderItems)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}