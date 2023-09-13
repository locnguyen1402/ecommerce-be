namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLBook : OLEntity
{
    public int Number_of_pages { get; set; } = 0;
    public List<string> Contributions { get; set; } = new List<string>();
    public List<OLKey> Works { get; set; } = new List<OLKey>();
    public string? Publish_country { get; set; }
    public string? Publish_date { get; set; }
}

public class OLBookMapperProfile : Profile
{
    public OLBookMapperProfile()
    {
        CreateMap<OLBook, Book>()
            .IncludeBase<OLEntity, BaseEntity>();
    }
}