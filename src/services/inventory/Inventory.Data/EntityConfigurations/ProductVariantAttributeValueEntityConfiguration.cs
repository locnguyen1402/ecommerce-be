using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ProductVariantAttributeValueEntityConfiguration : BaseEntityConfiguration<ProductVariantAttributeValue>
{
    public override void Configure(EntityTypeBuilder<ProductVariantAttributeValue> builder)
    {
        base.Configure(builder);

        builder.HasAlternateKey(p => new { p.ProductVariantId, p.ProductAttributeId });

        builder.Property(p => p.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(p => p.ProductVariant)
            .WithMany(p => p.ProductVariantAttributeValues)
            .HasForeignKey(p => p.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.ProductAttribute)
            .WithMany(p => p.ProductVariantAttributeValues)
            .HasForeignKey(p => p.ProductAttributeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.AttributeValue)
            .WithMany(p => p.ProductVariantAttributeValues)
            .HasForeignKey(p => p.AttributeValueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}