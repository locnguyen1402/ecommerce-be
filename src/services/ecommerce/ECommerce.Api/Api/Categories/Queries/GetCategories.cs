using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Categories.Specifications;
using ECommerce.Api.Categories.Responses;

namespace ECommerce.Api.Categories.Queries;

public class GetCategoriesQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCategoriesSpecification<CategoryResponse>(
            CategoryProjection.ToCategoryResponse()
            , keyword
            , pagingQuery
            );

        var items = await categoryRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
