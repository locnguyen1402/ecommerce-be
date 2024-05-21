using MediatR;

using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Products.Queries;

public record GetProductByIdQuery(string id) : IRequest<Product?>
{
    public string Id { get; set; } = id;
};

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.Query.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id, cancellationToken);
    }
}