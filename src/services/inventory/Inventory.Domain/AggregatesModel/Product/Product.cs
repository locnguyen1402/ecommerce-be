using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Product(string name, string slug) : Entity()
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    private readonly List<Category> _categories = [];
    public ICollection<Category> Categories => _categories;
    private readonly List<CategoryProduct> _categoryProducts = [];
    public ICollection<CategoryProduct> CategoryProducts => _categoryProducts;
    public Product(string name, string slug, decimal price, int quantity) : this(name, slug)
    {
        Price = price;
        Quantity = quantity;
    }
    public void UpdatePrice(decimal price)
    {
        Price = price;
    }
}