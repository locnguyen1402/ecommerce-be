using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Inventory.Data.EntityConfigurations;

public class MerchantProductEntityConfiguration : BaseEntityConfiguration<MerchantProduct>
{
    public override void Configure(EntityTypeBuilder<MerchantProduct> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Id);

        builder.HasKey(p => new { p.MerchantId, p.ProductId });
    }
}