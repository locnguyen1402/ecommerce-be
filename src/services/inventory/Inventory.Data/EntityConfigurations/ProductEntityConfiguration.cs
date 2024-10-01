using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ProductEntityConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.Property(p => p.Sku)
            .HasMaxLength(100);

        builder.HasMany(p => p.ProductAttributes)
            .WithMany(p => p.Products)
            .UsingEntity<ProductProductAttribute>(
                p =>
                {
                    p.HasKey(ppa => new { ppa.ProductId, ppa.ProductAttributeId });

                    p.HasOne(ppa => ppa.Product)
                        .WithMany(p => p.ProductProductAttributes)
                        .HasForeignKey(ppa => ppa.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

                    p.HasOne(ppa => ppa.ProductAttribute)
                        .WithMany(p => p.ProductProductAttributes)
                        .HasForeignKey(ppa => ppa.ProductAttributeId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );
    }
}