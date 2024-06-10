using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Categories.Responses;

public record CategoryResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description
);

public static class CategoryProjection
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return ToCategoryResponse().Compile().Invoke(category);
    }

    public static Expression<Func<Category, CategoryResponse>> ToCategoryResponse()
        => x =>
        new CategoryResponse(
            x.Id,
            x.Name,
            x.Slug,
            x.Description
        );
}
