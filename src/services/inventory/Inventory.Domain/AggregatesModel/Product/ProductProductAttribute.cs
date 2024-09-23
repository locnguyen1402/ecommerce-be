namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductProductAttribute
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public Guid ProductAttributeId { get; set; }
    public ProductAttribute? ProductAttribute { get; set; }
}
