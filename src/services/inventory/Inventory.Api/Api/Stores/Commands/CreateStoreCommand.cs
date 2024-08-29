using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Stores.Requests;

namespace ECommerce.Inventory.Api.Stores.Commands;

public class CreateStoreCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateStoreRequest request,
        IValidator<CreateStoreRequest> validator,
        IMerchantRepository merchantRepository,
        IStoreRepository storeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (!await merchantRepository.AnyAsync(x => x.Id == request.MerchantId, cancellationToken))
        {
            return Results.BadRequest("Merchant not found");
        }

        if (await storeRepository.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            return Results.BadRequest("Slug is already taken");
        }

        var newStore = new Store(request.Name, request.Slug, request.MerchantId);

        newStore.Update(
            request.Name
            , request.Description
            , request.PhoneNumber);

        await storeRepository.AddAndSaveChangeAsync(newStore, cancellationToken);

        return TypedResults.Ok(new IdResponse(newStore.Id.ToString()));
    };
}