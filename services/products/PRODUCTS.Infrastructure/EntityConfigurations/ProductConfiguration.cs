namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.PublicationYear)
            .HasColumnType("smallint")
            .HasDefaultValue(0);

        builder.Property(e => e.Publisher)
            .HasMaxLength(100)
            .HasDefaultValue("''");

        builder.Property(e => e.ImageUrlS)
            .HasColumnType("text");

        builder.Property(e => e.ImageUrlM)
            .HasColumnType("text");

        builder.Property(e => e.ImageUrlL)
            .HasColumnType("text");

        // builder.HasOne(p => p.ProductSaleInfo)
        //     .WithOne(p => p.Product)
        //     .HasForeignKey<ProductSaleInfo>(p => p.ProductId);
    }
}