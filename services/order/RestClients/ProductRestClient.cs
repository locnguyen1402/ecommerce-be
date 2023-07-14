using RestSharp;

namespace ECommerce.Services.Orders;

public class ProductRestClient : IProductRestClient
{
    private RestClient client;
    public ProductRestClient(string baseUrl)
    {
        client = new RestClient(baseUrl);
    }

    public async Task<RestResponse<ProductInfo[]>> GetProductInfos(List<Guid> ids)
    {
        var request = new RestRequest($"api/product/order");

        request.AddBody(new
        {
            Ids = ids,
        });

        return await client.ExecutePostAsync<ProductInfo[]>(request);
    }
}

public class ProductInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
}