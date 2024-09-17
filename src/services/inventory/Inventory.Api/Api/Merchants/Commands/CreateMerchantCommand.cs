using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class CreateMerchantCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateMerchantRequest request,
        IValidator<CreateMerchantRequest> validator,
        IMerchantRepository merchantRepository,
        ICategoryRepository categoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await merchantRepository.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            return Results.BadRequest("Slug is already taken");
        }

        var newMerchant = new Merchant(request.Name, request.Slug);

        newMerchant.Update(
            request.Name
            , request.Slug
            , request.Description);

        if (request.CategoryIds.Count > 0)
        {
            var categorySpec = new GetCategoriesByIdsSpecification(request.CategoryIds);
            var categories = await categoryRepository.GetAsync(categorySpec, cancellationToken);

            if (categories.Count() != request.CategoryIds.Count)
            {
                return Results.BadRequest("Some categories are not existed");
            }

            newMerchant.AddOrUpdateCategories(categories);
        }

        await merchantRepository.AddAndSaveChangeAsync(newMerchant, cancellationToken);

        return TypedResults.Ok(new IdResponse(newMerchant.Id.ToString()));
    };
}