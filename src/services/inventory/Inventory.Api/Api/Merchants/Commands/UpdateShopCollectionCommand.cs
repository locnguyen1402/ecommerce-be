using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using Microsoft.EntityFrameworkCore;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class UpdateShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateShopCollectionRequest request,
        IValidator<UpdateShopCollectionRequest> validator,
        IShopCollectionRepository shopCollectionRepository,
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

        if (await shopCollectionRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Slug is already existed");
        }

        var shopCollection = await shopCollectionRepository.Query
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (shopCollection == null)
        {
            return Results.NotFound("ShopCollection not found");
        }

        shopCollection.UpdateGeneralInfo(
            request.Name
            , string.IsNullOrEmpty(request.Slug) ? shopCollection.Slug : request.Slug
            , request.Description);

        shopCollection.ChangeParent(request.ParentId);

        await shopCollectionRepository.UpdateAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.NoContent();
    };
}