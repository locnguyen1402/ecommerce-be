using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Products.Queries;

public record GetProductsQuery(PagingQuery PagingQuery) : IRequest<List<Product>>
{
    public PagingQuery PagingQuery { get; init; } = PagingQuery;
};

public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.Query.AsNoTracking();

        var response = await query
                            .Skip((request.PagingQuery.PageIndex - 1) * request.PagingQuery.PageSize)
                            .Take(request.PagingQuery.PageSize)
                            .ToListAsync(cancellationToken);

        return response;
    }
}