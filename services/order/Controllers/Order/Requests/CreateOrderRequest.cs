namespace ECommerce.Services.Orders;

public class CreateOrderRequest
{
    public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Quantity { get; set; }
}