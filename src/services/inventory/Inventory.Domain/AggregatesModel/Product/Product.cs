using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Product : Entity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}