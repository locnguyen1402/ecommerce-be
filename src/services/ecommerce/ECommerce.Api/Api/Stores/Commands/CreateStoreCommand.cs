using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Stores.Requests;
using ECommerce.Api.Services;
using ECommerce.Shared.Common.Exceptions;

namespace ECommerce.Api.Stores.Commands;

public class CreateStoreCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateStoreRequest request,
        IMerchantService merchantService,
        IValidator<CreateStoreRequest> validator,
        IStoreRepository storeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            //return Results.ValidationProblem(validationResult.ToDictionary());
            throw new BadRequestException("errorCode", "One or more validation errors occurred", validationResult.ToDictionary());
        }

        if (await storeRepository.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            //return Results.BadRequest("Slug is already taken");
            throw new BadRequestException("errorCode", "Slug is already taken");
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);

        var newStore = new Store(request.Name, request.Slug, merchantId);

        newStore.Update(
            request.Name
            , request.Description
            , request.PhoneNumber);

        await storeRepository.AddAndSaveChangeAsync(newStore, cancellationToken);

        return TypedResults.Ok(new IdResponse(newStore.Id.ToString()));
    };
}