using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;

namespace ECommerce.Inventory.Api.Products.Queries;

public class GetProductAttributesQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        IProductAttributeRepository productAttributeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProductAttributesSpecification(
            keyword
            , pagingQuery
            );

        var items = await productAttributeRepository.PaginateAsync(spec, cancellationToken);

        items.PopulatePaginationInfo();

        return TypedResults.Ok(items);
    };
}
