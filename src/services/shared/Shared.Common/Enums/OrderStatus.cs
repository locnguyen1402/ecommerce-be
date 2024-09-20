namespace ECommerce.Shared.Common.Enums;

public enum OrderStatus
{
    UNSPECIFIED = 0,
    TO_PAY = 1,
    TO_SHIP = 2,
    TO_RECEIVE = 3,
    COMPLETED = 4,
    CANCELLED = 5,
    REFUND = 6,
}
