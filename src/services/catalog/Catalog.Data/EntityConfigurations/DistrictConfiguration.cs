using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Catalog.Domain.AggregatesModel.AdministrativeUnits;

namespace ECommerce.Catalog.Data.EntityConfigurations;

internal class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(p => p.Name);
    }
}