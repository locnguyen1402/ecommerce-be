using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public class ProductSaleInfo : Entity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; } = 0;
}