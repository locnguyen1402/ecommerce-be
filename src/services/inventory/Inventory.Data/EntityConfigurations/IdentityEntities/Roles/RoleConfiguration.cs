﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Inventory.Domain.AggregatesModel.Identity;

namespace ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

public class RoleConfiguration : BaseEntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        // Index for "normalized" role name to allow efficient lookups
        builder.HasIndex(t => t.NormalizedName)
            .IsUnique(true);

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(t => t.ConcurrencyStamp)
            .IsConcurrencyToken();

        builder
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(RoleConstants.Name.MaxLength)
            .HasDefaultValueSql("''");

        builder
            .Property(t => t.NormalizedName)
            .IsRequired()
            .HasMaxLength(RoleConstants.Name.MaxLength)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Predefined)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(RoleConstants.Description.MaxLength)
            .HasDefaultValueSql("''");

        builder
            .Property(x => x.Permissions)
            .HasColumnType("text[]");
    }
}
