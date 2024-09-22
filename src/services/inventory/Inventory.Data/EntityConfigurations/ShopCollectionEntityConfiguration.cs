using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class ShopCollectionEntityConfiguration : BaseEntityConfiguration<ShopCollection>
{
    public override void Configure(EntityTypeBuilder<ShopCollection> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(i => i.Slug)
            .IsUnique();
            
        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .HasDefaultValueSql("''");

        builder.HasMany(p => p.ShopCollections)
            .WithOne(c => c.Parent)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Products)
            .WithMany(c => c.ShopCollections)
            .UsingEntity<ShopCollectionProduct>(
                p =>
                {
                    p.HasKey(cp => new { cp.ShopCollectionId, cp.ProductId });

                    p.HasOne(cp => cp.ShopCollection)
                        .WithMany(c => c.ShopCollectionProducts)
                        .HasForeignKey(cp => cp.ShopCollectionId)
                        .OnDelete(DeleteBehavior.Cascade);

                    p.HasOne(cp => cp.Product)
                        .WithMany(c => c.ShopCollectionProducts)
                        .HasForeignKey(cp => cp.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );
    }
}