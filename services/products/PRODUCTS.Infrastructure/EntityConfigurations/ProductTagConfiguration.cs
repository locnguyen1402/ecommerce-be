namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.HasKey(pt => new { pt.ProductId, pt.TagId });

        builder.HasOne(p => p.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(p => p.ProductId);

        builder.HasOne(p => p.Tag)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(p => p.TagId);
    }
}