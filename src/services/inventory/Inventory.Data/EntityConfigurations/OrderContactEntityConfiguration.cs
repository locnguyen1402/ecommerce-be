using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class OrderContactEntityConfiguration : BaseEntityConfiguration<OrderContact>
{
    public override void Configure(EntityTypeBuilder<OrderContact> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ContactName)
            .HasMaxLength(200);

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(p => p.AddressInfo)
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'{}'");

        builder.Property(p => p.Notes)
            .HasMaxLength(500);
    }
}