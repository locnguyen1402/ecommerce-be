using MediatR;

using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Product?>
{
    public Guid Id { get; set; } = Id;
};

public class GetProductByIdQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<GetProductByIdQuery, Product?>
{
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await productRepository.Query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}