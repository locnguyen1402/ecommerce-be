using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLEntity
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public dynamic? Description { get; set; }
    public OLKey Type { get; set; } = null!;
    public List<string> Subjects { get; set; } = new List<string>();
    public List<string> Subject_people { get; set; } = new List<string>();
    public List<string> Subject_times { get; set; } = new List<string>();
    public List<int> Covers { get; set; } = new List<int>();
    public OLTypeValue? Created { get; set; }
    public OLTypeValue? Last_modified { get; set; }
}
public class OLKey
{
    public string Key { get; set; } = string.Empty;
}
public class OLTypeValue
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class OLEntityMapperProfile : Profile
{
    public OLEntityMapperProfile()
    {
        CreateMap<OLEntity, BaseEntity>()
            .ForMember(be => be.Id, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityId(ol)))
            .ForMember(be => be.RefType, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityType(ol)))
            .ForMember(be => be.Description, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityDescription(ol)))
            .ForMember(be => be.SubjectPeople, opt => opt.MapFrom(ol => ol.Subject_people))
            .ForMember(be => be.SubjectTimes, opt => opt.MapFrom(ol => ol.Subject_times))
            .ForMember(be => be.CreatedAt, opt =>
            {
                opt.PreCondition(o => o.Created is not null);
                opt.MapFrom(ol => ol.Created!.Value);
            })
            .ForMember(be => be.ImageUrlS, opt => opt.MapFrom((source) => OLMapperUtils.BuildImageSource(source, OLImageSize.S)))
            .ForMember(be => be.ImageUrlM, opt => opt.MapFrom((source) => OLMapperUtils.BuildImageSource(source, OLImageSize.M)));
    }
}

public class OLMapperUtils
{
    public static string GetEntityId(OLEntity source)
    {
        string olKey = source.Key;

        olKey = olKey.Replace("/works/", "");
        olKey = olKey.Replace("/books/", "");

        return olKey;
    }
    public static RefType GetEntityType(OLEntity source)
    {
        string olKey = source.Key;

        return olKey switch
        {
            string a when a.Contains("books", StringComparison.OrdinalIgnoreCase) => RefType.BOOK,
            string b when b.Contains("works", StringComparison.OrdinalIgnoreCase) => RefType.WORK,
            _ => RefType.OTHER,
        };
    }
    public static string? GetEntityDescription(OLEntity source)
    {
        string? result = null;

        if (source.Description is not null)
        {
            var description = source.Description;

            if (description.ValueKind.ToString() == "String")
            {
                result = description.ToString();
            }
            else
            {
                OLTypeValue des = JsonSerializer.Deserialize<OLTypeValue>(description.ToString(), JsonConstant.JsonSerializerOptions);

                result = des.Value;
            }
        }

        return result;
    }
    public static string? BuildImageSource(OLEntity source, OLImageSize imgSize)
    {
        if (source.Covers.IsNullOrEmpty())
        {
            return null;
        }

        string imgSrc = $"{OLConstants.IMAGE_BASE_URL}/b/id/{source.Covers[0]}-{imgSize}.jpg";

        return imgSrc;
    }
}