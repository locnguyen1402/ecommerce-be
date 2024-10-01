using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ProductVariantEntityConfiguration : BaseEntityConfiguration<ProductVariant>
{
    public override void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Stock)
            .IsRequired()
            .HasDefaultValue(0);

        builder.ToTable(t => t.HasCheckConstraint("CK_ProductVariant_Stock", "\"stock\" >= 0"));

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.Property(p => p.Sku)
            .HasMaxLength(100);

        builder.HasOne(p => p.Product)
            .WithMany(p => p.ProductVariants)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}