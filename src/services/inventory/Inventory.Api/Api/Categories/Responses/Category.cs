using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Categories.Responses;

public record CategoryResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description
);

public record AdminCategoryDetailResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description
)
{
    public AdminCategoryDetailResponse(
        Guid id,
        string name,
        string slug,
        string description,
        Category? categoryParent
    ) : this(id, name, slug, description)
    {
        if (categoryParent != null)
        {
            Parent = categoryParent.ToCategoryResponse();
        }
    }

    public CategoryResponse? Parent { get; }
};

public record CategoryOption(
    Guid Id,
    string Name
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

    public static Expression<Func<Category, CategoryOption>> ToCategoryOption()
        => x =>
        new CategoryOption(
            x.Id,
            x.Name
        );

    public static Expression<Func<Category, AdminCategoryDetailResponse>> ToAdminCategoryDetailResponse()
        => x =>
        new AdminCategoryDetailResponse(
            x.Id,
            x.Name,
            x.Slug,
            x.Description,
            x.Parent
        );
}
