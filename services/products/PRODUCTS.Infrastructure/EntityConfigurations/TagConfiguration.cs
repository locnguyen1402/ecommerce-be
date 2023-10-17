namespace ECommerce.Products.Infrastructure.EntityConfigurations;
public class TagConfiguration : BaseEntityConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Value)
            .HasMaxLength(100)
            .IsRequired();
    }
}