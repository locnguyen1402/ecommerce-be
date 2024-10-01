using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Data.EntityConfigurations;

public class CustomerEntityConfiguration : BaseEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.LastName)
            .HasMaxLength(200);

        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.UserName)
            .HasMaxLength(200);

        builder
            .Property(t => t.Gender)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{Gender.UNSPECIFIED}'");

        builder
            .Property(t => t.LevelType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{CustomerLevelType.SILVER}'");

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(100);
    }
}