using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

public class PermissionConfiguration : BaseEntityConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.Value)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .HasDefaultValueSql("''");

        builder.HasOne(t => t.Group)
            .WithMany(t => t.Permissions)
            .HasForeignKey(t => t.GroupId)
            .IsRequired();

        builder.HasIndex(t => t.Value)
            .IsUnique(true)
            .IncludeProperties(t => t.Name);
    }
}
