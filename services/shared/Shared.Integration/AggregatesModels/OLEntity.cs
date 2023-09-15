namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLEntity
{
    public string Key { get; set; } = string.Empty;
    public string? Error { get; set; }
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
            .ForMember(be => be.Id, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityId(ol.Key)))
            .ForMember(be => be.RefType, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityType(ol.Key)))
            .ForMember(be => be.Description, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityDescription(ol)))
            .ForMember(be => be.SubjectPeople, opt => opt.MapFrom(ol => ol.Subject_people))
            .ForMember(be => be.SubjectTimes, opt => opt.MapFrom(ol => ol.Subject_times))
            .ForMember(be => be.CreatedAt, opt =>
            {
                opt.PreCondition(o => o.Created is not null);
                opt.MapFrom(ol => ol.Created!.Value);
            })
            .ForMember(be => be.ImageUrlS, opt => opt.MapFrom((source) => (OLMapperUtils.BuildOLImageSources(source.Covers, OLImageSize.S) ?? new List<string>()).FirstOrDefault()))
            .ForMember(be => be.ImageUrlM, opt => opt.MapFrom((source) => (OLMapperUtils.BuildOLImageSources(source.Covers, OLImageSize.M) ?? new List<string>()).FirstOrDefault()));
    }
}

public class OLMapperUtils
{
    public static string GetEntityId(string olEntityKey)
    {
        string olKey = olEntityKey;

        olKey = olKey.Replace("/works/", "");
        olKey = olKey.Replace("/books/", "");

        return olKey;
    }
    public static RefType GetEntityType(string olEntityKey)
    {
        string olKey = olEntityKey;

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
    public static List<string>? BuildOLImageSources(List<int>? ids, OLImageSize imgSize)
    {
        if (ids.IsNullOrEmpty())
        {
            return null;
        }

        var sources = new List<string>();


        foreach (var id in ids!)
        {
            sources.Add(BuildOLImageSource(id, imgSize)!);
        }

        return sources;
    }
    public static string? BuildOLImageSource(int? id, OLImageSize imgSize)
    {
        if (id is null)
        {
            return null;
        }

        string imgSrc = $"{OLConstants.IMAGE_BASE_URL}/b/id/{id}-{imgSize}.jpg";

        return imgSrc;
    }
    public static string? BuildArchiveImageSource(string? id)
    {
        if (id.IsNullOrEmpty())
        {
            return null;
        }

        return $"{OLConstants.INTERNET_ARCHIVE_IMAGE_BASE_URL}/{id}";
    }
    public static List<string>? BuildArchiveImageSources(List<string>? ids)
    {
        if (ids.IsNullOrEmpty())
        {
            return null;
        }

        var sources = new List<string>();

        foreach (var id in ids!)
        {
            sources.Add(BuildArchiveImageSource(id)!);
        }

        return sources;
    }
}