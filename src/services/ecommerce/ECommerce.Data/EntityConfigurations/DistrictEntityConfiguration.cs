using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Data.EntityConfigurations;

public class DistrictEntityConfiguration : BaseEntityConfiguration<District>
{
    public override void Configure(EntityTypeBuilder<District> builder)
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
           .HasOne(p => p.Province)
           .WithMany(p => p.Districts)
           .HasForeignKey(p => p.ProvinceId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
