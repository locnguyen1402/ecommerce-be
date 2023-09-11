using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public class ProductSaleInfo : Entity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; private set; } = 0;
    public decimal Price { get; private set; } = 0;
    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void UpdatePrice(decimal price)
    {
        Price = price;
    }
}