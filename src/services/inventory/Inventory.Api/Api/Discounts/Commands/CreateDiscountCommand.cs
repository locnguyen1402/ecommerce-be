using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Discounts.Requests;
using ECommerce.Shared.Common.Enums;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Discounts.Commands;

public class CreateDiscountCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateDiscountRequest request,
        IValidator<CreateDiscountRequest> validator,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IDiscountRepository discountRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await discountRepository.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            return Results.BadRequest("Slug is already taken");
        }

        var newDiscount = new Discount(request.Name, request.Slug, request.Code);

        newDiscount.Update(
            request.Description
            , request.DiscountValue
            , request.MinOrderValue
            , request.MaxDiscountAmount
            , request.StartDate
            , request.EndDate);

        newDiscount.SetDiscountType(request.DiscountType);
        newDiscount.SetDiscountUnit(request.DiscountUnit);
        newDiscount.SetLimitation(request.LimitationTimes, request.LimitationType);

        // List<Product> selectedProducts = [];
        // if (request.Type == DiscountType.ASSIGNED_TO_PRODUCT && request.AppliedEntityIds.Count > 0)
        // {
        //     var productsSpec = new GetProductByIdsSpecification([.. request.AppliedEntityIds]);
        //     selectedProducts = (await productRepository.GetAsync(productsSpec, cancellationToken)).ToList();

        //     newDiscount.AddOrUpdateAppliedToProducts(selectedProducts);
        // }

        // List<Category> selectedCategories = [];
        // if (request.Type == DiscountType.ASSIGNED_TO_CATEGORY && request.AppliedEntityIds.Count > 0)
        // {
        //     var categoriesSpec = new GetCategoriesByIdsSpecification([.. request.AppliedEntityIds]);
        //     selectedCategories = (await categoryRepository.GetAsync(categoriesSpec, cancellationToken)).ToList();

        //     newDiscount.AddOrUpdateAppliedToCategories(selectedCategories);
        // }

        await discountRepository.AddAndSaveChangeAsync(newDiscount, cancellationToken);

        return TypedResults.Ok(new IdResponse(newDiscount.Id.ToString()));
    };
}