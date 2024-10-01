using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class CategoryEntityConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .HasDefaultValueSql("''");
    }
}