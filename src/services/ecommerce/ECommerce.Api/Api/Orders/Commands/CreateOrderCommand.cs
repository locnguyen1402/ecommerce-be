using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Orders.Requests;
using ECommerce.Shared.Common.AggregatesModel.Common;
using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Libs.Extensions;
using ECommerce.Api.Services;

namespace ECommerce.Api.Orders.Commands;

public class CreateOrderCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateOrderRequest request,
        IOrderRepository orderRepository,
        IStoreRepository storeRepository,
        IProductVariantRepository productVariantRepository,
        IProductService productService,
        ICustomerService customerService,
        IMerchantService merchantService,
        IValidator<CreateOrderRequest> validator,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        // Update stock by product variants
        var productItems = request.ProductItems.ToDictionary(x => x.ProductVariantId, x => x.Quantity);
        var updatedProductItems = productService.UpdateStockByProductVariantsAsync(productItems, cancellationToken);
        // TODO: Handle in case of not enough stock 

        var customer = await customerService.GetCustomerInfoAsync(cancellationToken);

        var newOrder = new Order(
            customer.Id,
            request.PhoneNumber
        );

        var orderNumber = string.Empty.ToGenerateOrderNumber();

        var addressInfo = new AddressInfo(
            request.OrderContact.ProvinceId,
            request.OrderContact.ProvinceName,
            request.OrderContact.ProvinceCode,
            request.OrderContact.DistrictId,
            request.OrderContact.DistrictName,
            request.OrderContact.DistrictCode,
            request.OrderContact.WardId,
            request.OrderContact.WardName,
            request.OrderContact.WardCode,
            request.OrderContact.AddressLine1,
            request.OrderContact.AddressLine2,
            request.OrderContact.AddressLine3
        );

        var orderContact = new OrderContact(
            request.OrderContact.ContactName,
            request.OrderContact.PhoneNumber,
            addressInfo,
            request.OrderContact.Notes,
            newOrder.Id
        );

        newOrder.SetPaymentMethod(request.PaymentMethod);

        var orderItems = MapToOrderItem(request.ProductItems);
        newOrder.AddOrderItems(orderItems);
        newOrder.AddOrderStatus(OrderStatus.TO_PAY);

        newOrder.SetDeliveryFee(request.DeliveryFee);
        newOrder.SetOrderContact(orderContact);

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);
        newOrder.SetMerchant(merchantId);
        newOrder.SetOrderNumber(orderNumber);

        await orderRepository.AddAndSaveChangeAsync(newOrder, cancellationToken);

        return TypedResults.Ok(new IdResponse(newOrder.Id.ToString()));
    };

    public static List<OrderItem> MapToOrderItem(List<ProductItemRequest> products)
    {
        var orderItems = new List<OrderItem>();

        products
            .ForEach(product =>
            {
                var orderItem = MapToPosOrderItem(product);

                if (orderItem != null)
                    orderItems.Add(orderItem);

            });

        return orderItems;
    }

    public static OrderItem MapToPosOrderItem(ProductItemRequest product)
    {

        var orderItem = new OrderItem(
            product.ProductId
            , product.ProductVariantId
            , product.UnitPrice
            , product.Quantity
        );

        if (product.VatPercent.HasValue)
            orderItem.SetVatPercent(product.VatPercent.Value);

        orderItem.SetProductInfo(product.ProductName, product.ListPrice);

        return orderItem;
    }
}