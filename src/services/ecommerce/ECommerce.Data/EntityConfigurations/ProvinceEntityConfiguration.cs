using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Data.EntityConfigurations;

public class ProvinceEntityConfiguration : BaseEntityConfiguration<Province>
{
    public override void Configure(EntityTypeBuilder<Province> builder)
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
    }
}
