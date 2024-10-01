using FluentValidation;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Merchants.Requests;
using ECommerce.Api.Services;

namespace ECommerce.Api.Merchants.Commands;

public class RemoveProductsFromShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        RemoveProductsFromShopCollectionRequest request,
        IValidator<RemoveProductsFromShopCollectionRequest> validator,
        IMerchantService merchantService,
        IShopCollectionRepository shopCollectionRepository,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        if (id != request.ShopCollectionId)
        {
            return Results.BadRequest();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (request.ProductIds.Count == 0)
        {
            return TypedResults.NoContent();
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);
        var shopCollection = await shopCollectionRepository.Query
            .Include(p => p.ShopCollectionProducts)
            .FirstOrDefaultAsync(p => p.Id == request.ShopCollectionId && merchantId == p.MerchantId, cancellationToken);

        if (shopCollection == null)
        {
            return Results.NotFound("ShopCollection not found");
        }

        shopCollection.RemoveProducts(request.ProductIds);

        await shopCollectionRepository.UpdateAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.NoContent();
    };
}