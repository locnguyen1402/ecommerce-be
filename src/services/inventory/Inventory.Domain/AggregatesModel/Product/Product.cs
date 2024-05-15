namespace ECommerce.Inventory.Domain.AggregatesModel.Product;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}