using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Categories.Specifications;
using ECommerce.Inventory.Api.Categories.Responses;

namespace ECommerce.Inventory.Api.Categories.Queries;

public class GetCategoryOptionsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCategoriesSpecification<CategoryOption>(
            CategoryProjection.ToCategoryOption()
            , keyword
            , pagingQuery
            );

        var items = await categoryRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
