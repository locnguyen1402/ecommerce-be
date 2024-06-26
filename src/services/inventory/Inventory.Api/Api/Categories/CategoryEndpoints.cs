using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Categories.Commands;
using ECommerce.Inventory.Api.Categories.Queries;

namespace ECommerce.Inventory.Api.Endpoints;

public class CategoryEndpoints(WebApplication app) : MinimalEndpoint(app, "/categories")
{
    public override void MapEndpoints()
    {
        Builder.MapGet<GetCategoriesQueryHandler>("/");
        Builder.MapPost<CreateCategoryCommandHandler>("/");

        Builder.MapGet<GetCategoryOptionsQueryHandler>("/options");

        Builder.MapGet<GetCategoryByIdQueryHandler>("/{id:Guid}");
        Builder.MapPut<UpdateCategoryCommandHandler>("/{id:Guid}");

        Builder.MapGet<GetCategoryBySlugQueryHandler>("/{slug}");
    }
}