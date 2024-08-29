using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Stores.Requests;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Stores.Commands;

public class UpdateStoreCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateStoreRequest request,
        IValidator<UpdateStoreRequest> validator,
        IMerchantRepository merchantRepository,
        IStoreRepository storeRepository,
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

        if (!await merchantRepository.AnyAsync(x => x.Id == request.MerchantId, cancellationToken))
        {
            return Results.BadRequest("Merchant not found");
        }

        if (await storeRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Slug is already existed");
        }

        var store = await storeRepository.Query
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (store == null)
        {
            return Results.NotFound();
        }

        store.Update(
            request.Name
            , request.Description
            , request.PhoneNumber);

        store.SetMerchant(request.MerchantId);

        await storeRepository.UpdateAndSaveChangeAsync(store, cancellationToken);

        return TypedResults.NoContent();
    };
}