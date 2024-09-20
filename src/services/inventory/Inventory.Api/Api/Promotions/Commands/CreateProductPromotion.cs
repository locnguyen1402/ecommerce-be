using Microsoft.EntityFrameworkCore;
using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Enums;

using ECommerce.Inventory.Api.Promotions.Requests;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Services;

namespace ECommerce.Inventory.Api.Promotions.Commands;

public class CreateProductPromotionCommandHandler : IEndpointHandler
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

        var isPromotionItemsValid = CheckProductPromotionItems(products, request.Items, ProductPromotionType.NORMAL);
        if (!isPromotionItemsValid)
        {
            return Results.BadRequest("Promotion items are not valid");
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);
        var items = request.Items.SelectMany(x => MapToProductPromotionItems(x, products.First(p => p.Id == x.ProductId)));
        var newPromotion = new ProductPromotion(
            request.Name,
            "request.Slug",
            request.StartDate,
            request.EndDate,
            merchantId
        );

        newPromotion.SetNormalPromotion();
        newPromotion.SetItems(items.ToList());

        await productPromotionRepository.AddAndSaveChangeAsync(newPromotion, cancellationToken);

        return TypedResults.Ok(new IdResponse(newPromotion.Id.ToString()));
    };

    public static List<ProductPromotionItem> MapToProductPromotionItems(CreateProductPromotionItemRequest request, Product product)
    {
        var items = request.Variants.Select(variantItem =>
        {
            var productVariant = product.ProductVariants.First(x => x.Id == variantItem.ProductVariantId);
            var item = new ProductPromotionItem(
                request.ProductId,
                variantItem.ProductVariantId
            );

            item.SetDiscount(
                productVariant.Price,
                variantItem.DiscountPrice,
                variantItem.DiscountPercentage
            );

            item.SetQuantity(variantItem.Quantity);
            item.SetNoProductsPerOrderLimit(variantItem.NoProductsPerOrderLimit, variantItem.MaxItemsPerOrder);

            return item;
        });

        return items.ToList();
    }

    public static bool CheckProductPromotionItems(List<Product> products, List<CreateProductPromotionItemRequest> promotionProducts, ProductPromotionType type)
    {
        var productIds = products.Select(x => x.Id).ToHashSet();
        var isProductsValid = productIds.OrderBy(x => x).SequenceEqual(promotionProducts.Select(x => x.ProductId).OrderBy(x => x));

        if (!isProductsValid)
            return false;

        var isPromotionItemsValid = products.All(x => CheckProductPromotionItem(x, promotionProducts.First(p => p.ProductId == x.Id), type));

        return isPromotionItemsValid;
    }

    private static bool CheckProductPromotionItem(Product product, CreateProductPromotionItemRequest promotionProduct, ProductPromotionType type)
    {
        var productVariantIds = product.ProductVariants.Select(x => x.Id).ToHashSet();
        var isVariantsValid = productVariantIds.OrderBy(x => x).SequenceEqual(promotionProduct.VariantIds.OrderBy(x => x));

        if (product.Id != promotionProduct.ProductId || !isVariantsValid)
            return false;

        var isPromotionItemsValid = promotionProduct.Variants.All(x => CheckProductVariantPromotionItem(product.ProductVariants.First(pv => pv.Id == x.ProductVariantId), x, type));

        return isPromotionItemsValid;
    }

    private static bool CheckProductVariantPromotionItem(ProductVariant productVariant, CreateProductVariantPromotionItemRequest promotionProductVariant, ProductPromotionType type)
    {
        var isValid = promotionProductVariant.DiscountPrice >= 0
                && promotionProductVariant.DiscountPrice <= productVariant.Price
                && promotionProductVariant.DiscountPercentage > 0
                && promotionProductVariant.DiscountPercentage <= 100
                && promotionProductVariant.Quantity >= 0;

        if (type == ProductPromotionType.FLASH_SALE)
        {
            isValid = isValid
                && promotionProductVariant.DiscountPercentage >= 5
                && promotionProductVariant.DiscountPercentage <= 90;
        }
        else if (
            type == ProductPromotionType.NORMAL
        )
        {
            isValid = isValid
                && promotionProductVariant.DiscountPercentage >= 0
                && promotionProductVariant.DiscountPercentage <= 50;
        }

        return isValid;
    }
}