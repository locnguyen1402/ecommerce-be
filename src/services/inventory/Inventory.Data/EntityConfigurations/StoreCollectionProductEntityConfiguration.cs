using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class StoreCollectionProductEntityConfiguration : BaseEntityConfiguration<StoreCollectionProduct>
{
    public override void Configure(EntityTypeBuilder<StoreCollectionProduct> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Id);

        builder.HasKey(p => new { p.StoreCollectionId, p.ProductId });

        builder.HasOne(p => p.StoreCollection)
            .WithMany(c => c.StoreCollectionProducts)
            .HasForeignKey(c => c.StoreCollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany(c => c.StoreCollectionProducts)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}