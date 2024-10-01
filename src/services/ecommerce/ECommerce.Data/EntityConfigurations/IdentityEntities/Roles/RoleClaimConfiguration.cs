using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel.Identity;

namespace ECommerce.Data.EntityConfigurations.IdentityEntities;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder
            .Property(t => t.ClaimType)
            .HasMaxLength(200)
            .HasDefaultValueSql("''");

        builder
            .Property(t => t.ClaimValue)
            .HasMaxLength(200)
            .HasDefaultValueSql("''");

        // Each Role can have many associated RoleClaims
        builder.HasOne(t => t.Role)
            .WithMany(t => t.RoleClaims)
            .HasForeignKey(t => t.RoleId)
            .IsRequired();
    }
}
