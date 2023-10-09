namespace ECommerce.Products.Domain.AggregatesModels;
public class Product : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Price { get; private set; }
    public string Tags { get; private set; } = string.Empty;
    public Guid ProductCategoryId { get; private set; }
    public ProductCategory ProductCategory { get; private set; } = null!;
    public Product(string name) : base()
    {
        Name = name;
    }
}