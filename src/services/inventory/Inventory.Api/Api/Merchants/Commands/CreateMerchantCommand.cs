using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class CreateMerchantCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateMerchantRequest request,
        IValidator<CreateMerchantRequest> validator,
        IMerchantRepository merchantRepository,
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
            , request.Description);

        await merchantRepository.AddAndSaveChangeAsync(newMerchant, cancellationToken);

        return TypedResults.Ok(new IdResponse(newMerchant.Id.ToString()));
    };
}