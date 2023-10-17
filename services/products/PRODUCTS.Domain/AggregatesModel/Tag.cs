namespace ECommerce.Products.Domain.AggregatesModels;
public class Tag : Entity
{
    public string Value { get; private set; } = null!;
    public List<Product> Products { get; } = new();
}