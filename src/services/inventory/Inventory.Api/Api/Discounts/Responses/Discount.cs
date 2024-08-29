using System.Linq.Expressions;

using ECommerce.Inventory.Api.Categories.Responses;
using ECommerce.Inventory.Api.Products.Responses;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Api.Discounts.Responses;

public record DiscountResponse(
    Guid Id,
    string Name,
    string Code,
    string? Description,
    DiscountType Type,
    decimal? DiscountValue,
    decimal? MinOrderValue,
    decimal? MaxDiscountAmount,
    DateTimeOffset? StartDate,
    DateTimeOffset? EndDate,
    bool IsActive,
    int? LimitationTimes,
    DiscountLimitationType? LimitationType
)
{
};

public static class DiscountProjection
{
    public static DiscountResponse ToDiscountResponse(this Discount discount)
    {
        return ToDiscountResponse().Compile().Invoke(discount);
    }

    public static Expression<Func<Discount, DiscountResponse>> ToDiscountResponse()
        => x =>
        new DiscountResponse(
            x.Id
            , x.Name
            , x.Code
            , x.Description ?? string.Empty
            , x.DiscountType
            , x.DiscountValue
            , x.MinOrderValue
            , x.MaxDiscountAmount
            , x.StartDate
            , x.EndDate
            , x.IsActive
            , x.LimitationTimes
            , x.LimitationType
        // , x.AppliedToCategories
        //     .AsQueryable()
        //     .Select(CategoryProjection.ToCategoryResponse())
        //     .ToList()
        // , x.AppliedToProducts
        //     .AsQueryable()
        //     .Select(ProductProjection.ToAdminProductDetailResponse())
        //     .ToList()
        );
}