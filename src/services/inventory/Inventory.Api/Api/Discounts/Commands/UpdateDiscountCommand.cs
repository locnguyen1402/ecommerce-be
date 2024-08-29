using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Discounts.Requests;
using ECommerce.Shared.Common.Enums;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Categories.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Discounts.Commands;

public class UpdateDiscountCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateDiscountRequest request,
        IValidator<UpdateDiscountRequest> validator,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IDiscountRepository discountRepository,
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

        if (await discountRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Slug is already existed");
        }

        var discount = await discountRepository.Query
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (discount == null)
        {
            return Results.NotFound();
        }

        discount.Update(
            request.Description
            , request.DiscountValue
            , request.MinOrderValue
            , request.MaxDiscountAmount
            , request.StartDate
            , request.EndDate);

        // List<Product> selectedProducts = [];
        // if (request.Type == DiscountType.ASSIGNED_TO_PRODUCT && request.AppliedEntityIds.Count > 0)
        // {
        //     var productsSpec = new GetProductByIdsSpecification([.. request.AppliedEntityIds]);
        //     selectedProducts = (await productRepository.GetAsync(productsSpec, cancellationToken)).ToList();

        //     if (discount.Type == DiscountType.ASSIGNED_TO_CATEGORY)
        //     {
        //         // Remove all applied products if the discount type is changed from category to product
        //         discount.AddOrUpdateAppliedToCategories([]);
        //     }

        //     discount.AddOrUpdateAppliedToProducts(selectedProducts);
        // }

        // List<Category> selectedCategories = [];
        // if (request.Type == DiscountType.ASSIGNED_TO_CATEGORY && request.AppliedEntityIds.Count > 0)
        // {
        //     var categoriesSpec = new GetCategoriesByIdsSpecification([.. request.AppliedEntityIds]);
        //     selectedCategories = (await categoryRepository.GetAsync(categoriesSpec, cancellationToken)).ToList();

        //     if (discount.Type == DiscountType.ASSIGNED_TO_PRODUCT)
        //     {
        //         // Remove all applied categories if the discount type is changed from product to category
        //         discount.AddOrUpdateAppliedToProducts([]);
        //     }
        //     discount.AddOrUpdateAppliedToCategories(selectedCategories);
        // }

        discount.SetDiscountType(request.DiscountType);
        discount.SetDiscountUnit(request.DiscountUnit);
        discount.SetLimitation(request.LimitationTimes, request.LimitationType);

        await discountRepository.UpdateAndSaveChangeAsync(discount, cancellationToken);

        return TypedResults.NoContent();
    };
}