namespace ECommerce.Services.Orders;

public interface IProductRestClient
{
    Task<ProductInfo?> GetProductInfo(Guid id);
}