using ECommerce.Shared.Libs.Domain;

namespace ECommerce.Domain.AggregatesModel;

public class OrderPromotionCondition : ValueObject
{
    public decimal Value { get; private set; }
    public int NoOfItems { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NoOfItems;
        yield return Value;
    }
}