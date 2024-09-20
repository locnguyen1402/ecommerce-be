using Microsoft.EntityFrameworkCore;
using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Enums;

using ECommerce.Inventory.Api.Promotions.Requests;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Services;
using ECommerce.Shared.Libs.Extensions;

namespace ECommerce.Inventory.Api.Promotions.Commands;

public class CreateFlashSalePromotionHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductPromotionRequest request,
        IValidator<CreateProductPromotionRequest> validator,
        IMerchantService merchantService,
        IProductPromotionRepository productPromotionRepository,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var products = await productRepository.Query
                        .Where(x => request.ProductIds.Contains(x.Id)).ToListAsync(cancellationToken);

        if (products.Count != request.ProductIds.Count)
        {
            return Results.BadRequest("Some products are not found");
        }

        var isPromotionItemsValid = CreateProductPromotionCommandHandler.CheckProductPromotionItems(products, request.Items, ProductPromotionType.FLASH_SALE);
        if (!isPromotionItemsValid)
        {
            return Results.BadRequest("Promotion items are not valid");
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);
        var items = request.Items
            .SelectMany(x => CreateProductPromotionCommandHandler.MapToProductPromotionItems(x, products.First(p => p.Id == x.ProductId)))
            .ToList();

        var newPromotion = new ProductPromotion(
            request.Name,
            request.Name.ToGenerateRandomSlug(),
            request.StartDate,
            request.EndDate,
            merchantId
        );

        newPromotion.SetFlashSalePromotion();
        newPromotion.SetItems(items);

        await productPromotionRepository.AddAndSaveChangeAsync(newPromotion, cancellationToken);

        return TypedResults.Ok(new IdResponse(newPromotion.Id.ToString()));
    };

}