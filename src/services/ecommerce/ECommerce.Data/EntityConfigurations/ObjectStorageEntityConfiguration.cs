using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Data.EntityConfigurations;

public class ObjectStorageEntityConfiguration : BaseEntityConfiguration<ObjectStorage>
{
    public override void Configure(EntityTypeBuilder<ObjectStorage> builder)
    {
        base.Configure(builder);
    }
}