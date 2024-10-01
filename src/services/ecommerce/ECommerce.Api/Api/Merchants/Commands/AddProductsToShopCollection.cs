using FluentValidation;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Merchants.Requests;
using ECommerce.Api.Services;

namespace ECommerce.Api.Merchants.Commands;

public class AddProductsToShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        AddProductsToShopCollectionRequest request,
        IValidator<AddProductsToShopCollectionRequest> validator,
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

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);
        var shopCollection = await shopCollectionRepository.Query
            .FirstOrDefaultAsync(p => p.Id == request.ShopCollectionId && merchantId == p.MerchantId, cancellationToken);

        if (shopCollection == null)
        {
            return Results.NotFound("ShopCollection not found");
        }

        var products = await productRepository.Query
            .Where(p => request.ProductIds.Contains(p.Id) && merchantId == p.MerchantId)
            .ToListAsync(cancellationToken);

        shopCollection.AddProducts(products);

        await shopCollectionRepository.UpdateAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.NoContent();
    };
}