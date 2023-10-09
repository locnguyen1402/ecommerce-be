namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class ProductCategoryConfiguration : BaseEntityConfiguration<ProductCategory>
{
    public override void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(250);
    }
}