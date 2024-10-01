using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Domain.AggregatesModel;

public abstract class EntityWithDiscounts : AuditedAggregateRoot, IDiscountable
{
    public readonly List<Discount> _appliedDiscounts = [];

    /// <inheritdoc />
    public bool HasDiscountsApplied { get; set; }

    /// <summary>
    /// Gets or sets the applied discounts.
    /// </summary>
    public IReadOnlyCollection<Discount> AppliedDiscounts => _appliedDiscounts;
}