namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(p => p.ProductCategory)
            .WithMany(p => p.Products);
    }
}