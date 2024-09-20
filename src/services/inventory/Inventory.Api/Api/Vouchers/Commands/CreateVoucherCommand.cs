using Microsoft.EntityFrameworkCore;
using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Enums;

using ECommerce.Inventory.Api.Vouchers.Requests;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Services;
using ECommerce.Inventory.Api.Vouchers.Specifications;

namespace ECommerce.Inventory.Api.Vouchers.Commands;

public class CreateVoucherCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateVoucherRequest request,
        IValidator<CreateVoucherRequest> validator,
        IVoucherRepository voucherRepository,
        IMerchantService merchantService,
        IProductRepository productRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var voucherSpec = new CheckExistedVoucherByCodeSpecification(request.Code);
        var existedVoucher = await voucherRepository.FindAsync(voucherSpec, cancellationToken);

        if (existedVoucher != null)
        {
            return Results.BadRequest("Voucher is already existed");
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);
        var newVoucher = new Voucher(
            request.Name,
            request.Code
        );

        newVoucher.SetMerchant(merchantId);
        newVoucher.SetPopularType(request.PopularType);
        newVoucher.SetTargetCustomerType(request.TargetCustomerType);
        newVoucher.SetValidity(request.MaxQuantity, request.MaxQuantityPerUser, request.MinSpend);
        newVoucher.SetPeriod(request.StartDate, request.EndDate);
        newVoucher.SetDiscountInfo(request.Type, request.DiscountType, request.Value, request.MaxValue);

        if (request.AppliedOnType == VoucherAppliedOnType.ALL_PRODUCTS)
        {
            newVoucher.SetAllProducts();
        }
        else
        {
            var products = await productRepository.Query
                .Where(x => request.ProductIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (products.Count != request.ProductIds.Count)
            {
                return Results.BadRequest("Some products are not found");
            }

            newVoucher.SetProducts(products);
        }

        await voucherRepository.AddAndSaveChangeAsync(newVoucher, cancellationToken);

        return TypedResults.Ok(new IdResponse(newVoucher.Id.ToString()));
    };
}