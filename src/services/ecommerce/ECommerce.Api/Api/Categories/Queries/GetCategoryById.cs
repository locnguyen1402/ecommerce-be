using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Categories.Specifications;
using ECommerce.Api.Categories.Responses;

namespace ECommerce.Api.Categories.Queries;

public class GetCategoryByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCategoryByIdSpecification<AdminCategoryDetailResponse>(id, CategoryProjection.ToAdminCategoryDetailResponse());

        var category = await categoryRepository.FindAsync(spec, cancellationToken);

        if (category is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(category);
    };
}
