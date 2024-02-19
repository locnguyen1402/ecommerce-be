using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Catalog.Domain.AggregatesModel.AdministrativeUnits;

namespace ECommerce.Catalog.Data.EntityConfigurations;

internal class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(p => p.Name);
    }
}