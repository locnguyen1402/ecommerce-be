using ECommerce.Shared.Libs.Domain;

namespace ECommerce.Shared.Common.AggregatesModel.Common;


public class AddressInfo : ValueObject
{
    /// <summary>
    /// Gets or sets the provinceId.
    /// </summary>
    public Guid ProvinceId { get; set; }

    /// <summary>
    /// Gets or sets the province name.
    /// </summary>
    public string ProvinceName { get; set; }

    /// <summary>
    /// Gets or sets the provinceCode.
    /// </summary>
    public string? ProvinceCode { get; set; }

    /// <summary>
    /// Gets or sets the districtId.
    /// </summary>
    public Guid DistrictId { get; set; }

    /// <summary>
    /// Gets or sets the district.
    /// </summary>
    public string DistrictName { get; set; }

    /// <summary>
    /// Gets or sets the districtCode.
    /// </summary>
    public string? DistrictCode { get; set; }

    /// <summary>
    /// Gets or sets the wardId.
    /// </summary>
    public Guid WardId { get; set; }

    /// <summary>
    /// Gets or sets the wardName.
    /// </summary>
    public string WardName { get; set; }

    /// <summary>
    /// Gets or sets the wardCode.
    /// </summary>
    public string? WardCode { get; set; }

    /// <summary>
    /// Gets or sets the street address line 1.
    /// Here, you must include primary information, including the street address.
    /// </summary>
    public string AddressLine1 { get; set; }

    /// <summary>
    /// Gets or sets the street address line 2.
    /// Here, you can include additional information, such as the apartment number.
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Gets or sets the street address line 3.
    /// Here, you can include additional information, such as the apartment number.
    /// </summary>
    public string? AddressLine3 { get; set; }

    public string? FullAddress { get; set; }

    // Empty constructor in this case is required by EF Core,
    // because has a complex type as a parameter in the default constructor.
    public AddressInfo()
    {
        ProvinceId = Guid.Empty;
        ProvinceName = string.Empty;
        ProvinceCode = string.Empty;
        DistrictId = Guid.Empty;
        DistrictName = string.Empty;
        DistrictCode = string.Empty;
        WardId = Guid.Empty;
        WardName = string.Empty;
        WardCode = string.Empty;

        AddressLine1 = string.Empty;
        AddressLine2 = string.Empty;
        AddressLine3 = string.Empty;
        FullAddress = string.Empty;
    }

    public AddressInfo(
        Guid provinceId, string provinceName, string? provinceCode,
        Guid districtId, string districtName, string? districtCode,
        Guid wardId, string wardName, string? wardCode,
        string addressLine1, string? addressLine2, string? addressLine3)
    {
        ProvinceId = provinceId;
        ProvinceName = provinceName;
        ProvinceCode = provinceCode;
        DistrictId = districtId;
        DistrictName = districtName;
        DistrictCode = districtCode;
        WardId = wardId;
        WardName = wardName;
        WardCode = wardCode;

        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        AddressLine3 = addressLine3;
        FullAddress = GetFullAddress();
    }

    public string GetFullAddress()
    {
        List<string?> list = [AddressLine1, AddressLine2, AddressLine3, WardName, DistrictName, ProvinceName];
        var fullAddress = string.Join(", ", list.Where(x => !string.IsNullOrEmpty(x)));

        return fullAddress;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return ProvinceId;
        yield return ProvinceName;
        yield return ProvinceCode ?? string.Empty;
        yield return DistrictId;
        yield return DistrictName;
        yield return DistrictCode ?? string.Empty;
        yield return WardId;
        yield return WardName;
        yield return WardCode ?? string.Empty;

        yield return AddressLine1;
        yield return AddressLine2 ?? string.Empty;
        yield return AddressLine3 ?? string.Empty;
    }
}
