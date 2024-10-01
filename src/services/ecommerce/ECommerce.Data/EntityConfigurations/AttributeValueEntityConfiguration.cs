using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;

namespace ECommerce.Data.EntityConfigurations;

public class AttributeValueEntityConfiguration : BaseEntityConfiguration<AttributeValue>
{
    public override void Configure(EntityTypeBuilder<AttributeValue> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(p => p.ProductAttribute)
            .WithMany(p => p.AttributeValues)
            .HasForeignKey(p => p.ProductAttributeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}