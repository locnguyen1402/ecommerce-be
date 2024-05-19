using ECommerce.Shared.Common.Endpoint;

namespace ECommerce.Inventory.Api.Endpoints;

public class ProductEndpoints : MinimalEndpoint
{
    public ProductEndpoints(IEndpointRouteBuilder builder) : base(builder, "/products") { }
    public override void MapEndpoints()
    {
        Builder.MapGet("/", () => "Hello from products");

        Builder.MapGet("/test", () => "Hello from test");
    }
}