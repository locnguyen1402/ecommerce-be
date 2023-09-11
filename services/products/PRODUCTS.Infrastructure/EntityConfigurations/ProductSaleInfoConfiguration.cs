namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class ProductSaleInfoConfiguration : BaseEntityConfiguration<ProductSaleInfo>
{
    public override void Configure(EntityTypeBuilder<ProductSaleInfo> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Quantity)
            .HasDefaultValue(0);

        builder.Property(p => p.Price)
            .HasDefaultValue(0);
    }
}