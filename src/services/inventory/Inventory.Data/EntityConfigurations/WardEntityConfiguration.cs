using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class WardEntityConfiguration : BaseEntityConfiguration<Ward>
{
    public override void Configure(EntityTypeBuilder<Ward> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired()
            .HasDefaultValueSql("''");

        builder
            .Property(p => p.Code)
            .HasMaxLength(100)
            .IsRequired()
            .HasDefaultValueSql("''");

        builder
            .Property(p => p.ZipCode)
            .HasMaxLength(100)
            .HasDefaultValueSql("''");

        builder
           .HasOne(p => p.District)
           .WithMany(p => p.Wards)
           .HasForeignKey(p => p.DistrictId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
