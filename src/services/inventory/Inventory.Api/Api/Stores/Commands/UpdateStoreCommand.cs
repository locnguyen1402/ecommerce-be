using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Stores.Requests;
using Microsoft.EntityFrameworkCore;
using ECommerce.Inventory.Api.Services;
using ECommerce.Shared.Common.Exceptions;

namespace ECommerce.Inventory.Api.Stores.Commands;

public class UpdateStoreCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateStoreRequest request,
        IMerchantService merchantService,
        IValidator<UpdateStoreRequest> validator,
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
            //return Results.ValidationProblem(validationResult.ToDictionary());
            throw new BadRequestException("errorCode", "One or more validation errors occurred", validationResult.ToDictionary());
        }

        if (await storeRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            //return Results.BadRequest("Slug is already existed");
            throw new BadRequestException("errorCode", "Slug is already taken");
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);

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

        store.SetMerchant(merchantId);

        await storeRepository.UpdateAndSaveChangeAsync(store, cancellationToken);

        return TypedResults.NoContent();
    };
}