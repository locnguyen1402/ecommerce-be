namespace ECommerce.Shared.Integration.Application.Responses;
public class OLWorkResponse
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public dynamic? Description { get; set; }
}

public class OLWorkDescription
{
    public string value { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
}

public class OLWorkType
{
    public string key { get; set; } = "/type/work";
}

public class WorkBookMapperProfile : Profile
{
    public WorkBookMapperProfile()
    {
        CreateMap<OLWorkResponse, Work>()
            .ForMember(b => b.Id, opt =>
            {
                opt.MapFrom(w => w.Key);
                opt.AddTransform(val => val.Replace("/works/", ""));
            })
            .ForMember(b => b.Description, opt => opt.MapFrom<OLWorkDescriptionResolver>());
    }
}

public class OLWorkDescriptionResolver : IValueResolver<OLWorkResponse, Work, string?>
{
    public string? Resolve(OLWorkResponse source, Work destination, string? member, ResolutionContext context)
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
                OLWorkDescription des = JsonSerializer.Deserialize<OLWorkDescription>(description.ToString());

                result = des.value;
            }
        }

        return result;
    }
}

public class OLWorkImageResolver : IValueResolver<OLWorkResponse, Work, string?>
{
    public OLWorkImageResolver()
    {
        
    }
    public string? Resolve(OLWorkResponse source, Work destination, string? member, ResolutionContext context)
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
                OLWorkDescription des = JsonSerializer.Deserialize<OLWorkDescription>(description.ToString());

                result = des.value;
            }
        }

        return result;
    }
}


