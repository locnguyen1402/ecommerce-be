using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Stores.Responses;

public record StoreResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsActive
);

public record AdminStoreDetailResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsActive
);

public static class StoreProjection
{
    public static StoreResponse ToStoreResponse(this Store store)
    {
        return ToStoreResponse().Compile().Invoke(store);
    }

    public static Expression<Func<Store, StoreResponse>> ToStoreResponse()
        => x =>
        new StoreResponse(
            x.Id
            , x.Name
            , x.Slug
            , x.Description
            , x.IsActive
        );

    public static Expression<Func<Store, AdminStoreDetailResponse>> ToAdminStoreDetailResponse()
        => x =>
        new AdminStoreDetailResponse(
            x.Id
            , x.Name
            , x.Slug
            , x.Description
            , x.IsActive
        );
}