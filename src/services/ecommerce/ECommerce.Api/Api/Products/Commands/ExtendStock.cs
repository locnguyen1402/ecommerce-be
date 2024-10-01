using FluentValidation;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Products.Requests;

namespace ECommerce.Api.Products.Commands;

public class ExtendStockCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        Guid variantId,
        ExtendStockRequest request,
        IValidator<ExtendStockRequest> validator,
        IProductVariantRepository productVariantRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var variant = await productVariantRepository.Query.FirstOrDefaultAsync(x => x.Id == request.ProductVariantId, cancellationToken);

        if (variant == null)
        {
            return Results.NotFound();
        }

        variant.IncreaseStock(request.Quantity);

        await productVariantRepository.UpdateAndSaveChangeAsync(variant, cancellationToken);

        return TypedResults.NoContent();
    };
}