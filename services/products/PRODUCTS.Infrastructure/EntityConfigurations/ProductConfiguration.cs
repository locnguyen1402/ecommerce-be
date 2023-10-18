namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(p => p.Products);

        builder.HasMany(p => p.Tags)
            .WithMany(p => p.Products)
            .UsingEntity<ProductTag>();
    }
}