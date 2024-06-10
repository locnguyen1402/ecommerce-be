using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Categories.Queries;

public class GetCategoriesQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        PagingQuery pagingQuery,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCategoriesSpecification(pagingQuery: pagingQuery);

        var categories = await categoryRepository.PaginateAsync(spec, cancellationToken);

        categories.PopulatePaginationInfo();

        return TypedResults.Ok(categories);
    };
}
