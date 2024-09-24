using System.Linq.Expressions;
using ECommerce.Inventory.Api.Customers.Requests;
using ECommerce.Shared.Common.AggregatesModel.Common;

namespace ECommerce.Inventory.Api.Customers.Responses;

public record AddressResponse(
    Guid ProvinceId,
    string ProvinceName,
    string ProvinceCode,
    Guid DistrictId,
    string DistrictName,
    string DistrictCode,
    Guid WardId,
    string WardName,
    string WardCode,
    string AddressLine1,
    string? AddressLine2,
    string? AddressLine3,
    string? FullAddress
);

public static class AddressProjection
{
    public static AddressResponse ToAddressResponse(this AddressInfo address)
    {
        return ToAddressResponse().Compile().Invoke(address);
    }

    public static List<AddressResponse>? ToListAddressResponse(this IEnumerable<AddressInfo> addresses)
    {
        return addresses.Any() ? addresses.Select(ToAddressResponse().Compile()).ToList() : null;
    }

    public static AddressInfo ToAddressInfo(this AddressRequest address)
    {
        return ToAddressInfo().Compile().Invoke(address);
    }

    public static Expression<Func<AddressInfo, AddressResponse>> ToAddressResponse()
        => x =>
        new AddressResponse(
            x.ProvinceId,
            x.ProvinceCode ?? string.Empty,
            x.ProvinceName,
            x.DistrictId,
            x.DistrictCode ?? string.Empty,
            x.DistrictName,
            x.WardId,
            x.WardCode ?? string.Empty,
            x.WardName,
            x.AddressLine1,
            x.AddressLine2,
            x.AddressLine3,
            x.FullAddress
        );

    public static Expression<Func<AddressRequest, AddressInfo>> ToAddressInfo()
        => x =>
        new AddressInfo(
            x.ProvinceId,
            x.ProvinceName,
            x.ProvinceCode,
            x.DistrictId,
            x.DistrictName,
            x.DistrictCode,
            x.WardId,
            x.WardName,
            x.WardCode,
            x.AddressLine1,
            x.AddressLine2,
            string.Empty
        );
}