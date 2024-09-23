using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel.Identity;

namespace ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // Primary key
        builder.HasKey(x => new { x.UserId, x.RoleId });

        // Each User can have many entries in the UserRole join table
        builder.HasOne(t => t.User)
            .WithMany(t => t.UserRoles)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        // Each Role can have many entries in the UserRole join table
        builder.HasOne(t => t.Role)
            .WithMany(t => t.UserRoles)
            .HasForeignKey(t => t.RoleId)
            .IsRequired();
    }
}
