using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Merchants.Responses;

public record MerchantResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsActive
)
{
};

public static class MerchantProjection
{
    public static MerchantResponse ToMerchantResponse(this Merchant merchant)
    {
        return ToMerchantResponse().Compile().Invoke(merchant);
    }

    public static Expression<Func<Merchant, MerchantResponse>> ToMerchantResponse()
        => x =>
        new MerchantResponse(
            x.Id
            , x.Name
            , x.Slug
            , x.Description
            , x.IsActive
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