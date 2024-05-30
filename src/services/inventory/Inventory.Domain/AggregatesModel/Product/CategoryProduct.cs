namespace ECommerce.Inventory.Domain.AggregatesModel;

public class CategoryProduct(Guid categoryId, Guid productId)
{
    public Guid CategoryId { get; init; } = categoryId;
    public Guid ProductId { get; init; } = productId;
    public Category? Category { get; private set; }
    public Product? Product { get; private set; }
}