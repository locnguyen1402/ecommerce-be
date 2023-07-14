namespace ECommerce.Services.Product;

public class OrderProductsRequest
{
    public List<Guid> Ids { get; set; } = new();
}