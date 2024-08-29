using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class DiscountEntityConfiguration : BaseEntityConfiguration<Discount>
{
    public override void Configure(EntityTypeBuilder<Discount> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasIndex(t => t.Code)
            .IsUnique();

        builder
            .Property(t => t.DiscountType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{DiscountType.UNSPECIFIED}'");

        builder
            .Property(t => t.DiscountUnit)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{DiscountUnit.UNSPECIFIED}'");

        builder
            .Property(t => t.LimitationType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{DiscountLimitationType.UNSPECIFIED}'");

        builder
            .Property(t => t.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(t => t.DiscountUsageHistory)
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'[]'");

        builder
            .Property(t => t.Description)
            .HasMaxLength(500)
            .HasDefaultValueSql("''");

        builder.HasMany(p => p.AppliedToCategories)
            .WithMany(c => c.AppliedDiscounts)
            .UsingEntity<Dictionary<string, object>>(
                "Discount_AppliedToCategories",
                c => c.HasOne<Category>()
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade),
                c => c.HasOne<Discount>()
                        .WithMany()
                        .HasForeignKey("Discount_Id")
                        .OnDelete(DeleteBehavior.Cascade),
                c =>
                {
                    c.HasKey("Discount_Id", "Category_Id");
                    c.HasIndex("Discount_Id");
                }
            );

        builder.HasMany(c => c.AppliedToProducts)
            .WithMany(c => c.AppliedDiscounts)
            .UsingEntity<Dictionary<string, object>>(
                "Discount_AppliedToProducts",
                c => c
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("Product_Id")
                    .OnDelete(DeleteBehavior.Cascade),
                c => c
                    .HasOne<Discount>()
                    .WithMany()
                    .HasForeignKey("Discount_Id")
                    .OnDelete(DeleteBehavior.Cascade),
                c =>
                {
                    c.HasIndex("Discount_Id");
                    c.HasKey("Discount_Id", "Product_Id");
                });
    }
}