using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ContactEntityConfiguration : BaseEntityConfiguration<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{AddressType.UNSPECIFIED}'");

        builder.Property(p => p.Name)
            .HasMaxLength(200);

        builder.Property(p => p.ContactName)
            .HasMaxLength(200);

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);

        builder.Property(p => p.IsDefault)
            .HasDefaultValue(true);

        builder.HasOne(p => p.Customer)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.AddressInfo)
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'{}'");
    }
}