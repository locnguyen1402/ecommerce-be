namespace ECommerce.Shared.Common.Enums;

public enum VoucherAppliedOnType
{
    UNSPECIFIED = 0,
    ALL_PRODUCTS = 1,
    SPECIFIC_PRODUCTS = 2,
}
public enum VoucherTargetCustomerType
{
    UNSPECIFIED = 0,
    ALL = 1,
    NEW = 2,
    REDEEM = 3,
}
public enum VoucherType
{
    UNSPECIFIED = 0,
    DISCOUNT = 1,
    COIN_BACK = 2,
    SHIPPING_FEE = 3,
}
public enum VoucherDiscountType
{
    UNSPECIFIED = 0,
    PERCENTAGE = 1,
    AMOUNT = 2,
}
public enum VoucherPopularType
{
    UNSPECIFIED = 0,
    PUBLIC = 1,
    PRIVATE = 2,
}

public enum VoucherStatus
{
    UNSPECIFIED,
    NEW,
    IN_PROCESS,
    FINISHED,
}