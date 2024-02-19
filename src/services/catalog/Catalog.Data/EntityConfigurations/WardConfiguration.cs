using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Catalog.Domain.AggregatesModel.AdministrativeUnits;

namespace ECommerce.Catalog.Data.EntityConfigurations;

internal class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        builder.HasKey(p => p.Name);
    }
}