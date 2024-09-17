using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations
{
    public class MerchantEntityConfiguration : BaseEntityConfiguration<Merchant>
    {
        public override void Configure(EntityTypeBuilder<Merchant> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder
                .Property(t => t.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("''");

            builder.HasMany(p => p.Categories)
                .WithMany(c => c.Merchants)
                .UsingEntity<MerchantCategory>(
                    p =>
                    {
                        p.HasKey(cp => new { cp.MerchantId, cp.CategoryId });

                        p.HasOne(cp => cp.Category)
                            .WithMany(c => c.MerchantCategories)
                            .HasForeignKey(cp => cp.CategoryId)
                            .OnDelete(DeleteBehavior.Cascade);

                        p.HasOne(cp => cp.Merchant)
                            .WithMany(c => c.MerchantCategories)
                            .HasForeignKey(cp => cp.MerchantId)
                            .OnDelete(DeleteBehavior.Cascade);

                    }
                );

            builder.HasMany(p => p.Products)
                 .WithOne(c => c.Merchant)
                 .HasForeignKey(c => c.MerchantId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ShopCollections)
                 .WithOne(c => c.Merchant)
                 .HasForeignKey(c => c.MerchantId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}