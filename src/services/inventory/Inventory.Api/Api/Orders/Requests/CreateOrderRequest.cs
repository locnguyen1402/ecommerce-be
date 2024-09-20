using ECommerce.Shared.Common.Enums;
using FluentValidation;

namespace ECommerce.Inventory.Api.Orders.Requests;

public class CreateOrderRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public decimal DeliveryFee { get; set; }
    public OrderContactRequest OrderContact { get; set; } = null!;
    public List<ProductItemRequest> ProductItems { get; set; } = [];
}

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    private string PrefixErrorMessage => nameof(CreateOrderRequestValidator);
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.OrderContact)
            .SetValidator(x => new OrderContactRequestValidator());

        RuleForEach(x => x.ProductItems)
            .SetValidator(x => new ProductItemRequestValidator());
    }
}