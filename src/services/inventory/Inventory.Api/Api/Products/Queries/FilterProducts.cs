using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Products.Responses;

namespace ECommerce.Inventory.Api.Products.Queries;

public class FilterProductsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        string[]? shopCollectionIds,
        string[]? notInShopCollectionIds,
        PagingQuery pagingQuery,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new FilterProductsSpecification<FilteredProductResponse>(
            ProductProjection.ToFilteredProductResponse()
            , keyword
            , pagingQuery
            , shopCollectionIds.ToQueryGuidList()
            , notInShopCollectionIds.ToQueryGuidList()
            );

        var items = await productRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
