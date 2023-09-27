using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLSearchResultItem
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("lending_edition_s")]
    public string? LendingEditionS { get; set; }
    [JsonPropertyName("cover_i")]
    public int? CoverImageId { get; set; }
    [JsonPropertyName("cover_edition_key")]
    public string? CoverEditionKey { get; set; }
    [JsonPropertyName("has_fulltext")]
    public bool HasFullText { get; set; } = false;
    [JsonPropertyName("ebook_access")]
    public string? EbookAccess { get; set; }
    [JsonPropertyName("want_to_read_count")]
    public int WantToReadCount { get; set; } = 0;
    [JsonPropertyName("already_read_count")]
    public int AlreadyReadCount { get; set; } = 0;
    [JsonPropertyName("ia")]
    public List<string>? FromArchiveImageIds { get; set; }
    [JsonPropertyName("first_publish_year")]
    public int? FirstPublishYear { get; set; }
    [JsonPropertyName("author_key")]
    public List<string>? AuthorIds { get; set; }
    [JsonPropertyName("author_name")]
    public List<string>? AuthorNames { get; set; }
    [JsonPropertyName("ratings_average")]
    public float RatingsAverage { get; set; } = 0;
    [JsonPropertyName("ratings_count")]
    public int RatingsCount { get; set; } = 0;
    [JsonPropertyName("edition_count")]
    public int EditionCount { get; set; } = 0;
    [JsonPropertyName("edition_key")]
    public List<string> EditionKeys { get; set; } = new List<string>();
}

public class OLSearchResultItemMapperProfile : Profile
{
    public OLSearchResultItemMapperProfile()
    {
        CreateMap<OLSearchResultItem, SearchResultItem>()
            .ForMember(be => be.Id, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityId(ol.Key)))
            .ForMember(be => be.RefType, opt => opt.MapFrom(ol => OLMapperUtils.GetEntityType(ol.Key)))
            .ForMember(be => be.LendingBookId, opt => opt.MapFrom(ol => ol.LendingEditionS))
            .ForMember(be => be.LendingBookStatus, opt => opt.MapFrom(ol => GetLendingBookStatus(ol)))
            .ForMember(be => be.CoverBookId, opt => opt.MapFrom(ol => ol.CoverEditionKey))
            .ForMember(be => be.CoverImageUrl, opt => opt.MapFrom(ol => OLMapperUtils.BuildOLImageSource(ol.CoverImageId, OLImageSize.M)))
            .ForMember(be => be.RelatedBookImgs, opt => opt.MapFrom(ol => OLMapperUtils.BuildArchiveImageSources(ol.FromArchiveImageIds)))
            .ForMember(be => be.Authors, opt => opt.MapFrom(ol => BuildAuthorInfos(ol)))
            .ForMember(be => be.FirstEditionId, opt =>
            {
                opt.PreCondition(o => !o.EditionKeys.IsNullOrEmpty());
                opt.MapFrom(o => OLMapperUtils.GetEntityId(o.EditionKeys.First()));
            });
    }
    private static BookStatus GetLendingBookStatus(OLSearchResultItem source)
    {
        string? status = source.EbookAccess;

        if (status.IsNullOrEmpty())
        {
            return BookStatus.OTHER;
        }

        return status switch
        {
            string a when a.Equals("no_ebook", StringComparison.OrdinalIgnoreCase) => BookStatus.NOVIEW,
            string b when b.Equals("borrowable", StringComparison.OrdinalIgnoreCase) => BookStatus.BORROW,
            string c when c.Equals("public", StringComparison.OrdinalIgnoreCase) => BookStatus.FULL,
            _ => BookStatus.OTHER,
        };
    }
    private static List<IdName> BuildAuthorInfos(OLSearchResultItem source)
    {
        if (source.AuthorIds.IsNullOrEmpty() || source.AuthorNames.IsNullOrEmpty() || (source.AuthorIds?.Count != source.AuthorNames?.Count))
        {
            return new List<IdName>();
        }

        return source.AuthorIds!.Select((id, index) => new IdName()
        {
            Id = id,
            Name = source.AuthorNames![index]
        }).ToList();
    }
}