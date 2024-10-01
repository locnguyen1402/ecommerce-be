using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Directories.Responses;

public record ProvinceResponse(
    Guid Id,
    string Name,
    string Code
);

public static class ProvinceProjection
{
    public static ProvinceResponse ToProvinceResponse(this Province Province)
    {
        return ToProvinceResponse().Compile().Invoke(Province);
    }

    public static Expression<Func<Province, ProvinceResponse>> ToProvinceResponse()
        => x =>
        new ProvinceResponse(
            x.Id
            , x.Name
            , x.Code
        );
}