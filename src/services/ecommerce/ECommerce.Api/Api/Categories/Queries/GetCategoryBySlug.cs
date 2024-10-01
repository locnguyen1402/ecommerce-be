using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Categories.Specifications;
using ECommerce.Api.Categories.Responses;

namespace ECommerce.Api.Categories.Queries;

public class GetCategoryBySlugQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string slug,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCategoryBySlugSpecification<CategoryResponse>(slug, CategoryProjection.ToCategoryResponse());

        var category = await categoryRepository.FindAsync(spec, cancellationToken);

        if (category is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(category);
    };
}
