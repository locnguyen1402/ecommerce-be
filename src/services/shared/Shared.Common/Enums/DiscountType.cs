namespace ECommerce.Shared.Common.Enums;

public enum DiscountType
{
    UNSPECIFIED,
    /// <summary>
    /// Assigned to product.
    /// </summary>
    ASSIGNED_TO_PRODUCT,
    /// <summary>
    /// Assigned to categories (all products in a category).
    /// </summary>
    ASSIGNED_TO_CATEGORY,
    /// <summary>
    /// Assigned to shipping.
    /// </summary>
    ASSIGNED_TO_SHIPPING,
    /// <summary>
    /// Assigned to order subtotal.
    /// </summary>
    ASSIGNED_TO_ORDER_SUBTOTAL,
}

