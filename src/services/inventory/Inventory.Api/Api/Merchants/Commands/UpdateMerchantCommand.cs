using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using Microsoft.EntityFrameworkCore;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class UpdateMerchantCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateMerchantRequest request,
        IValidator<UpdateMerchantRequest> validator,
        IMerchantRepository merchantRepository,
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

        if (await merchantRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Slug is already existed");
        }

        var merchant = await merchantRepository.Query
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (merchant == null)
        {
            return Results.NotFound();
        }

        if (request.CategoryIds.Count > 0)
        {
            var categorySpec = new GetCategoriesByIdsSpecification(request.CategoryIds);
            var categories = await categoryRepository.GetAsync(categorySpec, cancellationToken);

            if (categories.Count() != request.CategoryIds.Count)
            {
                return Results.BadRequest("Some categories are not existed");
            }

            var listMerchantCategories = new List<MerchantCategory>();
            foreach (var categoryId in request.CategoryIds)
            {
                var newMerchantCategory = new MerchantCategory(merchant.Id, categoryId);
                listMerchantCategories.Add(newMerchantCategory);
            }
            merchant.AddOrUpdateCategories(listMerchantCategories);
        }

        merchant.Update(
            request.Name
            , request.Slug
            , request.Description);

        await merchantRepository.UpdateAndSaveChangeAsync(merchant, cancellationToken);

        return TypedResults.NoContent();
    };
}