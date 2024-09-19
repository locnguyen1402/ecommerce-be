namespace ECommerce.Shared.Common.Enums;

public enum PromotionStatus
{
    UNSPECIFIED,
    NEW,
    IN_PROCESS,
    FINISHED,
}
public enum NoProductsPerOrderLimit
{
    UNSPECIFIED,
    UNLIMITED,
    SPECIFIED
}

public enum ProductPromotionType
{
    UNSPECIFIED,
    FLASH_SALE,
    NORMAL,
}

public enum OrderPromotionType
{
    UNSPECIFIED,
    BUNDLE,
    ADD_ON,
    GIFT
}
public enum OrderPromotionSubItemType
{
    UNSPECIFIED,
    ADD_ON,
    GIFT
}
public enum BundlePromotionDiscountType
{
    UNSPECIFIED,
    PERCENTAGE,
    AMOUNT,
    SPECIFIED
}