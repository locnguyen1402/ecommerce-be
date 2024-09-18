using System.Linq.Expressions;
using ECommerce.Inventory.Api.Categories.Responses;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Merchants.Responses;

public record MerchantResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsActive
);


public record AdminMerchantDetailResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsActive,
    List<CategoryResponse> Categories
);

public static class MerchantProjection
{
    public static MerchantResponse ToMerchantResponse(this Merchant merchant)
    {
        return ToMerchantResponse().Compile().Invoke(merchant);
    }

    public static AdminMerchantDetailResponse ToAdminMerchantDetailResponse(this Merchant merchant)
    {
        return ToAdminMerchantDetailResponse().Compile().Invoke(merchant);
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

    public static Expression<Func<Merchant, AdminMerchantDetailResponse>> ToAdminMerchantDetailResponse()
        => x =>
        new AdminMerchantDetailResponse(
            x.Id,
            x.Name,
            x.Slug,
            x.Description,
            x.IsActive,
            x.Categories
                .AsQueryable()
                .Select(CategoryProjection.ToCategoryResponse())
                .ToList()
        );
}