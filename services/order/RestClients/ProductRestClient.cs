using RestSharp;

namespace ECommerce.Services.Orders;

public class ProductRestClient : IProductRestClient
{
    private RestClient client;
    public ProductRestClient(string baseUrl)
    {
        client = new RestClient(baseUrl);
    }

    public async Task<ProductInfo?> GetProductInfo(Guid id)
    {
        var request = new RestRequest($"api/product/{id}");

        return await client.GetAsync<ProductInfo>(request);
    }
}

public class ProductInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
}