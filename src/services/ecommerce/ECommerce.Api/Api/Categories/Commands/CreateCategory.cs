
using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.AggregatesModel.Response;

using ECommerce.Api.Categories.Requests;
using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Categories.Commands;

public class CreateCategoryCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateCategoryRequest request,
        IValidator<CreateCategoryRequest> validator,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await categoryRepository.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            return Results.BadRequest("Slug is already taken");
        }

        var newCategory = new Category(request.Name, request.Slug, request.Description, request.ParentId);

        await categoryRepository.AddAndSaveChangeAsync(newCategory, cancellationToken);

        return TypedResults.Ok(new IdResponse(newCategory.Id.ToString()));

    };
}