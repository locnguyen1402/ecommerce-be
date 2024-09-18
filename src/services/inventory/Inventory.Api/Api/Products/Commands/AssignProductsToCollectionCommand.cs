using FluentValidation;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Requests;

namespace ECommerce.Inventory.Api.Products.Commands;

public class AssignProductToShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        AssignProductsToCollectionRequest request,
        IValidator<AssignProductsToCollectionRequest> validator,
        IProductRepository productRepository,
        IShopCollectionRepository shopCollectionRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        // TODO: get shop collection by merchant id in logged in user
        var shopCollection = await shopCollectionRepository.Query
            .FirstOrDefaultAsync(p => p.Id == request.ShopCollectionId, cancellationToken);

        if (shopCollection == null)
        {
            return Results.NotFound("ShopCollection not found");
        }

        // TODO: get products by merchant id in logged in user
        var products = await productRepository.Query
            .Where(p => request.ProductIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        shopCollection.ReplaceProducts(products);

        shopCollectionRepository.Update(shopCollection);

        await shopCollectionRepository.UpdateAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.NoContent();
    };
}