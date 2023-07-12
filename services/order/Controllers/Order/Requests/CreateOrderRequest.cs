namespace ECommerce.Services.Orders;

public class CreateOrderRequest
{
    public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}