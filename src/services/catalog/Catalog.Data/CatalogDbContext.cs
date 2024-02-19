using Microsoft.EntityFrameworkCore;

using ECommerce.Catalog.Domain.AggregatesModel.AdministrativeUnits;
using ECommerce.Catalog.Data.EntityConfigurations;

namespace ECommerce.Catalog.Data;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Ward> Wards => Set<Ward>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
        modelBuilder.ApplyConfiguration(new DistrictConfiguration());
        modelBuilder.ApplyConfiguration(new WardConfiguration());
    }
}
