namespace ECommerce.Products.Domain.AggregatesModels;
public class Tag : Entity
{
    public string Value { get; private set; } = null!;
    public List<Product> Products { get; } = new();
    public List<ProductTag> ProductTags { get; private set; } = new();
    public Tag(string value) : base()
    {
        Value = value;
    }
}