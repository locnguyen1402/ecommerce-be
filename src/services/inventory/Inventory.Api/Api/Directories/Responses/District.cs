using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Directories.Responses;

public record DistrictResponse(
    Guid Id,
    string Name,
    string Code
);

public record AdminDistrictResponse(
    Guid Id,
    string DistrictName,
    string DistrictCode,
    string ProvinceName,
    string ProvinceCode
);

public static class DistrictProjection
{
    public static DistrictResponse ToDistrictResponse(this District district)
    {
        return ToDistrictResponse().Compile().Invoke(district);
    }

    public static AdminDistrictResponse ToAdminDistrictResponse(this District district)
    {
        return ToAdminDistrictResponse().Compile().Invoke(district);
    }

    public static Expression<Func<District, DistrictResponse>> ToDistrictResponse()
        => x =>
        new DistrictResponse(
            x.Id
            , x.Name
            , x.Code
        );

    public static Expression<Func<District, AdminDistrictResponse>> ToAdminDistrictResponse()
        => x =>
        new AdminDistrictResponse(
            x.Id
            , x.Name
            , x.Code
            , x.Province.Name
            , x.Province.Code
        );
}