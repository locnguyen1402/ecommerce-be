using System;
using System.Collections.Generic;

namespace ECommerce.Services.Orders;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public virtual decimal TotalPrice => Quantity * UnitPrice;
}
