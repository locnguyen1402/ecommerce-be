using Microsoft.EntityFrameworkCore;

using MediatR;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Pagination;
using ECommerce.Shared.Common.Queries;

namespace ECommerce.Inventory.Api.Products.Queries;

public record GetProductsQuery(PagingQuery PagingQuery) : IRequest<IPaginatedList<Product>>
{
    public PagingQuery PagingQuery { get; init; } = PagingQuery;
};

public class GetProductsQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<GetProductsQuery, IPaginatedList<Product>>
{
    public async Task<IPaginatedList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = productRepository.Query.AsNoTracking();

        var response = await query
                            .ToPaginatedListAsync(request.PagingQuery.PageIndex, request.PagingQuery.PageSize, cancellationToken);

        response.PopulatePaginationInfo();

        return response;
    }
}