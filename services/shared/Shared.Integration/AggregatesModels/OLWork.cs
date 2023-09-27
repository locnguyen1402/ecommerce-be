namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLWork : OLEntity
{
    public List<string> Subjects { get; set; } = new List<string>();
    [JsonPropertyName("subject_people")]
    public List<string> SubjectPeople { get; set; } = new List<string>();
    [JsonPropertyName("subject_times")]
    public List<string> SubjectTimes { get; set; } = new List<string>();
    [JsonPropertyName("subject_places")]
    public List<string> SubjectPlaces { get; set; } = new List<string>();
}

public class OLWorkMapperProfile : Profile
{
    public OLWorkMapperProfile()
    {
        CreateMap<OLWork, Work>()
            .IncludeBase<OLEntity, BaseEntity>();
    }
}