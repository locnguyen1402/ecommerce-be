namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLBook : OLEntity
{
    public int Number_of_pages { get; set; } = 0;
    public List<string> Contributions { get; set; } = new List<string>();
    public List<OLKey> Works { get; set; } = new List<OLKey>();
    public string? Publish_country { get; set; }
    public string? Publish_date { get; set; }
    public List<string> Series { get; set; } = new List<string>();
    public List<string> Publishers { get; set; } = new List<string>();
    public List<string> Isbn_13 { get; set; } = new List<string>();
    public List<string> Isbn_10 { get; set; } = new List<string>();
    public BookStatus Status { get; private set; } = BookStatus.OTHER;
    public void UpdateStatus(BookStatus status)
    {
        Status = status;
    }
}

public class OLBookMapperProfile : Profile
{
    public OLBookMapperProfile()
    {
        CreateMap<OLBook, Book>()
            .IncludeBase<OLEntity, BaseEntity>()
            .ForMember(b => b.NumberOfPages, opt => opt.MapFrom(ol => ol.Number_of_pages))
            .ForMember(b => b.PublishCountry, opt => opt.MapFrom(ol => ol.Publish_country))
            .ForMember(b => b.PublishDate, opt => opt.MapFrom(ol => ol.Publish_date))
            .ForMember(b => b.Isbn13, opt => opt.MapFrom(ol => ol.Isbn_13))
            .ForMember(b => b.Isbn10, opt => opt.MapFrom(ol => ol.Isbn_10))
            .ForMember(b => b.WorkId, opt =>
            {
                opt.PreCondition(o => !o.Works.IsNullOrEmpty());
                opt.MapFrom(o => OLMapperUtils.GetEntityId(o.Works.First().Key));
            });
    }
}