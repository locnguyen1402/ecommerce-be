namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLWorkRatings
{
    public SummaryInfo? Summary { get; set; }
    public CountsInfo? Counts { get; set; }
    public class SummaryInfo
    {
        public float? Average { get; set; } = 0;
        public int? Count { get; set; } = 0;
    }
    public class CountsInfo
    {
        [JsonPropertyName("1")]
        public int Rating1Stars { get; set; } = 0;
        [JsonPropertyName("2")]
        public int Rating2Stars { get; set; } = 0;
        [JsonPropertyName("3")]
        public int Rating3Stars { get; set; } = 0;
        [JsonPropertyName("4")]
        public int Rating4Stars { get; set; } = 0;
        [JsonPropertyName("5")]
        public int Rating5Stars { get; set; } = 0;
    }
}

public class OLWorkRatingsProfile : Profile
{
    public OLWorkRatingsProfile()
    {
        CreateMap<OLWorkRatings, WorkRatings>()
            .ForMember(wr => wr.Average, opt => opt.MapFrom(ol => ol.Summary!.Average))
            .ForMember(wr => wr.Count, opt => opt.MapFrom(ol => ol.Summary!.Count))
            .ForMember(wr => wr.Rating1Stars, opt => opt.MapFrom(ol => ol.Counts!.Rating1Stars))
            .ForMember(wr => wr.Rating2Stars, opt => opt.MapFrom(ol => ol.Counts!.Rating2Stars))
            .ForMember(wr => wr.Rating3Stars, opt => opt.MapFrom(ol => ol.Counts!.Rating3Stars))
            .ForMember(wr => wr.Rating4Stars, opt => opt.MapFrom(ol => ol.Counts!.Rating4Stars))
            .ForMember(wr => wr.Rating5Stars, opt => opt.MapFrom(ol => ol.Counts!.Rating5Stars))
            .ForAllMembers(ol =>
            {
                ol.PreCondition(o => o.Summary is not null && o.Counts is not null);
            });
    }
}