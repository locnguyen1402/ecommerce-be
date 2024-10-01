using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Directories.Responses;

public record WardResponse(
    Guid Id,
    string Name,
    string Code
);

public record AdminWardResponse(
    Guid Id,
    string WardName,
    string WardCode,
    string DistrictName,
    string DistrictCode,
    string ProvinceName,
    string ProvinceCode
);

public static class WardProjection
{
    public static WardResponse ToWardResponse(this Ward ward)
    {
        return ToWardResponse().Compile().Invoke(ward);
    }

    public static AdminWardResponse ToAdminWardResponse(this Ward ward)
    {
        return ToAdminWardResponse().Compile().Invoke(ward);
    }

    public static Expression<Func<Ward, WardResponse>> ToWardResponse()
        => x =>
        new WardResponse(
            x.Id
            , x.Name
            , x.Code
        );

    public static Expression<Func<Ward, AdminWardResponse>> ToAdminWardResponse()
        => x =>
        new AdminWardResponse(
            x.Id
            , x.Name
            , x.Code
            , x.District.Name
            , x.District.Code
            , x.District.Province.Name
            , x.District.Province.Code
        );
}