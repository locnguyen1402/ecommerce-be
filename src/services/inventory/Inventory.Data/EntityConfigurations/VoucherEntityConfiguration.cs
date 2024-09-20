using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class VoucherEntityConfiguration : BaseEntityConfiguration<Voucher>
{
    public override void Configure(EntityTypeBuilder<Voucher> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(t => t.AppliedOnType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{VoucherAppliedOnType.UNSPECIFIED}'");

        builder
            .Property(t => t.TargetCustomerType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{VoucherTargetCustomerType.UNSPECIFIED}'");

        builder
            .Property(t => t.PopularType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{VoucherPopularType.UNSPECIFIED}'");

        builder.Property(p => p.MinSpend)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder.Property(p => p.MaxQuantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.MaxQuantityPerUser)
            .IsRequired()
            .HasDefaultValue(1);

        builder
            .Property(t => t.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{VoucherType.UNSPECIFIED}'");

        builder
            .Property(t => t.DiscountType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValueSql($"'{VoucherDiscountType.UNSPECIFIED}'");

        builder.Property(p => p.Value)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasDefaultValue(0);

        builder.Property(p => p.MaxValue)
            .HasPrecision(19, 2);

        builder.HasMany(p => p.Products)
            .WithMany(p => p.Vouchers)
            .UsingEntity<VoucherProduct>(
                p =>
                {
                    p.HasKey(vp => new { vp.VoucherId, vp.ProductId });

                    p.HasOne(vp => vp.Voucher)
                        .WithMany(p => p.VoucherProducts)
                        .HasForeignKey(vp => vp.VoucherId)
                        .OnDelete(DeleteBehavior.Cascade);

                    p.HasOne(vp => vp.Product)
                        .WithMany(p => p.VoucherProducts)
                        .HasForeignKey(vp => vp.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );
    }
}