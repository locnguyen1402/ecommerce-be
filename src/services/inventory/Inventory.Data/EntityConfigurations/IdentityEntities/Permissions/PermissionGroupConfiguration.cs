using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

public class PermissionGroupConfiguration : BaseEntityConfiguration<PermissionGroup>
{
    public override void Configure(EntityTypeBuilder<PermissionGroup> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .HasDefaultValueSql("''");
    }
}
