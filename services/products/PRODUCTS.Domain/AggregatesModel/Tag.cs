namespace ECommerce.Products.Domain.AggregatesModels;
public class Tag : Entity
{
    public string Value { get; private set; } = null!;
    public virtual List<Product> Products => ProductTags.Select(p => p.Product).ToList();
    public List<ProductTag> ProductTags { get; } = new();
    public Tag(string value) : base()
    {
        Value = value;
    }
}