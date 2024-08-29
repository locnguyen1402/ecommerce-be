namespace ECommerce.Inventory.Domain.AggregatesModel;

public class CategoryProduct()
{
    public CategoryProduct(Guid? categoryId, Guid? productId) : this()
    {
        CategoryId = categoryId;
        ProductId = productId;
    }
    public Guid? CategoryId { get; private set; }
    public Guid? ProductId { get; private set; }
    public Category? Category { get; private set; }
    public Product? Product { get; private set; }
}