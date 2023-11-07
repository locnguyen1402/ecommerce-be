namespace Products.MinimalApis.Modules;
public class OrdersModule : BaseModule
{
    public OrdersModule(IEndpointRouteBuilder builder) : base(builder, "/orders")
    {
    }
    public override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", () =>
        {
            return "Orders nè";
        });

        builder.MapGet("/{id}", (OrderQuery query) =>
        {
            return $"Order detail {query.id} nè";
        });
    }
}