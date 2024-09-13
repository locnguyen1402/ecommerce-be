using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class AssignProductToShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        AssignProductToShopCollectionRequest request,
        IValidator<AssignProductToShopCollectionRequest> validator,
        IMerchantRepository merchantRepository,
        IShopCollectionRepository shopCollectionRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var merchant = await merchantRepository.Query
            .Include(x => x.Products)
            .FirstOrDefaultAsync(p => p.Id == request.MerchantId, cancellationToken);

        if (merchant == null)
        {
            return Results.NotFound("Merchant not found");
        }

        var shopCollection = await shopCollectionRepository.Query
            .FirstOrDefaultAsync(p => p.Id == request.ShopCollectionId, cancellationToken);

        if (shopCollection == null)
        {
            return Results.NotFound("ShopCollection not found");
        }

        merchant.Products.ToList().ForEach(x =>
        {
            if (!request.ProductIds.Any(p => p == x.Id))
            {
                Results.BadRequest($"Product with Id ({x.Id}) is not existed in merchant");
            }
        });

        foreach (var productId in request.ProductIds)
        {
            var merchantProduct = merchant.Products.First(x => x.Id == productId);
            shopCollection.AddProduct(merchantProduct);
        }

        shopCollectionRepository.Update(shopCollection);

        await shopCollectionRepository.UpdateAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.NoContent();
    };
}