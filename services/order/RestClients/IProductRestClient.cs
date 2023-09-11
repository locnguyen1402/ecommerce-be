using RestSharp;

namespace ECommerce.Services.Orders;

public interface IProductRestClient
{
    Task<RestResponse<ProductInfo[]>> GetProductInfos(List<Guid> ids);
}