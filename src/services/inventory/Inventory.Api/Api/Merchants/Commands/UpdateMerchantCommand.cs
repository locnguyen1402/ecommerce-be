using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class UpdateMerchantCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateMerchantRequest request,
        IValidator<UpdateMerchantRequest> validator,
        IMerchantRepository merchantRepository,
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
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (merchant == null)
        {
            return Results.NotFound();
        }

        merchant.Update(
            request.Name
            , request.Description);

        await merchantRepository.UpdateAndSaveChangeAsync(merchant, cancellationToken);

        return TypedResults.NoContent();
    };
}