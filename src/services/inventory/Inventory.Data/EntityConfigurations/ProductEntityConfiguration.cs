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

        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.Quantity)
            .IsRequired();

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<CategoryProduct>(
                p => {
                    p.HasKey(cp => new { cp.CategoryId, cp.ProductId });

                    p.HasOne(cp => cp.Category)
                        .WithMany(c => c.CategoryProducts)
                        .HasForeignKey(cp => cp.CategoryId);

                    p.HasOne(cp => cp.Product)
                        .WithMany(c => c.CategoryProducts)
                        .HasForeignKey(cp => cp.ProductId);
                }
            );
    }
}