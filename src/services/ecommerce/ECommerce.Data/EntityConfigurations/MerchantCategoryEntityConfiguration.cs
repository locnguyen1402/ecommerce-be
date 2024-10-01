using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class MerchantCategoryEntityConfiguration : BaseEntityConfiguration<MerchantCategory>
{
    public override void Configure(EntityTypeBuilder<MerchantCategory> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Id);

        builder.HasKey(p => new { p.MerchantId, p.CategoryId });
    }
}