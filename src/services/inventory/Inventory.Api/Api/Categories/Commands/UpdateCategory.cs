using FluentValidation;

using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Categories.Requests;
using ECommerce.Inventory.Api.Categories.Responses;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Categories.Commands;

public class UpdateCategoryCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateCategoryRequest request,
        IValidator<UpdateCategoryRequest> validator,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        if (id != request.Id)
        {
            return Results.BadRequest();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var category = await categoryRepository.Query
                        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (category == null)
        {
            return Results.NotFound();
        }

        if (category.Slug != request.Slug && await categoryRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Slug is already existed");
        }

        category.UpdateGeneralInfo(request.Name, request.Slug, request.Description);

        if (request.ParentId != null && category.ParentId != request.ParentId)
        {
            var parentCategory = await categoryRepository.Query
                .FirstOrDefaultAsync(p => p.Id == request.ParentId, cancellationToken);

            if (parentCategory == null)
            {
                return Results.BadRequest("Parent category not found");
            }

            category.ChangeParent(request.ParentId);
        }
        else if (request.ParentId == null)
        {
            category.ChangeParent(null);
        }

        await categoryRepository.UpdateAndSaveChangeAsync(category, cancellationToken);

        return TypedResults.Ok(category.ToCategoryResponse());
    };
}