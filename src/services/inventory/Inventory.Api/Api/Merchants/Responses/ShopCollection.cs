using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Merchants.Responses;

public record ShopCollectionResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    List<ShopCollectionResponse> Children
);

public record ShopCollectionOption(
    Guid Id,
    string Name
);

public static class ShopCollectionProjection
{
    public static ShopCollectionResponse ToShopCollectionResponse(this ShopCollection collection)
    {
        return ToShopCollectionResponse().Compile().Invoke(collection);
    }

    public static List<ShopCollectionResponse>? ToListShopCollectionResponse(this IEnumerable<ShopCollection> shopCollections)
    {
        return shopCollections.Any() ? shopCollections.Select(ToShopCollectionResponse().Compile()).ToList() : null;
    }

    public static Expression<Func<ShopCollection, ShopCollectionResponse>> ToShopCollectionResponse()
        => x =>
        new ShopCollectionResponse(
            x.Id,
            x.Name,
            x.Slug,
            x.Description,
            x.ShopCollections.ToListShopCollectionResponse() ?? new List<ShopCollectionResponse>()
        );

    public static Expression<Func<ShopCollection, ShopCollectionOption>> ToShopCollectionOption()
        => x =>
        new ShopCollectionOption(
            x.Id,
            x.Name
        );
}