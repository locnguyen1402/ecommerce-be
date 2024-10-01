using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Categories.Commands;
using ECommerce.Api.Categories.Queries;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class CategoryEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/categories")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Categories");
    }
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