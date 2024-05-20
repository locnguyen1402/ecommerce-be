using MediatR;

namespace ECommerce.Inventory.Api.Products.Queries;

public record GetProductsQuery() : IRequest<string>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, string>
{
    public Task<string> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Hello from GetProductsQueryHandler");
    }
}