using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public abstract class EntityWithDiscounts : Entity, IDiscountable
{
    public readonly List<Discount> _appliedDiscounts = [];

    /// <inheritdoc />
    public bool HasDiscountsApplied { get; set; }

    /// <summary>
    /// Gets or sets the applied discounts.
    /// </summary>
    public IReadOnlyCollection<Discount> AppliedDiscounts => _appliedDiscounts;
}