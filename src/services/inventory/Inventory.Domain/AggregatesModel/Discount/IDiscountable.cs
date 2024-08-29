namespace ECommerce.Inventory.Domain.AggregatesModel;

/// <summary>
/// Represents an entity with applicable discounts.
/// </summary>
public interface IDiscountable
{
    /// <summary>
    /// Gets or sets a value indicating whether this entity has discounts applied.
    /// </summary>
    /// <remarks>
    /// We use this property for performance optimization:
    /// if this property is set to false, then we do not need to load AppliedDiscounts navigation property.
    /// </remarks>
    bool HasDiscountsApplied { get; set; }

    /// <summary>
    /// Gets the applied discounts.
    /// </summary>
    public IReadOnlyCollection<Discount> AppliedDiscounts { get; }
}
