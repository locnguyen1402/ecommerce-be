namespace ECommerce.Shared.Common.Enums;

/// <summary>
/// Represents a discount limitation type.
/// </summary>
public enum DiscountLimitationType
{
    UNSPECIFIED = 0,
    /// <summary>
    /// No limitation.
    /// </summary>
    UNLIMITED = 1,
    /// <summary>
    /// N times only.
    /// </summary>
    N_TIMES_ONLY = 2,
    /// <summary>
    /// N times per customer.
    /// </summary>
    N_TIMES_PER_CUSTOMER = 3
}