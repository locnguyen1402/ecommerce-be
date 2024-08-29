namespace ECommerce.Shared.Common.Enums;

public enum DiscountAppliedType
{
    UNSPECIFIED,
    /// <summary>
    /// Assigned to merchant.
    /// </summary>
    ASSIGNED_TO_MERCHANT,
    /// <summary>
    /// Assigned to categories (all products in a category).
    /// </summary>
    ASSIGNED_TO_CATEGORY,
    /// <summary>
    /// Assigned to payment method.
    /// </summary>
    ASSIGNED_TO_PAYMENT_METHOD,
    /// <summary>
    /// Assigned to all.
    /// </summary>
    ASSIGNED_TO_ALL,

    /// <summary>
    /// Assigned to shipping.
    /// </summary>
    ASSIGNED_TO_SHIPPING,
}

