using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ProductAttributeEntityConfiguration : BaseEntityConfiguration<ProductAttribute>
{
    public override void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}