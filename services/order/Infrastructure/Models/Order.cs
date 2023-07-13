using ECommerce.Shared.Common;

namespace ECommerce.Services.Orders;

public class Order : Entity
{
    public decimal TotalPrice { get; private set; }
    public int TotalQuantity { get; private set; }
    public List<OrderItem> OrderItems { get; set; }
    public Order() : base()
    {
        OrderItems = new List<OrderItem>();
    }
    public decimal CalcTotalPrice()
    {
        return OrderItems.Sum(t => t.TotalPrice);
    }
    public int CalcTotalQuantity()
    {
        return OrderItems.Sum(t => t.Quantity);
    }
    public void AddOrderItems(List<OrderItem> items)
    {
        OrderItems = items;
        TotalPrice = CalcTotalPrice();
        TotalQuantity = CalcTotalQuantity();
    }
}