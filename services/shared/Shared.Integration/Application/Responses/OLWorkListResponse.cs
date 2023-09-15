namespace ECommerce.Shared.Integration.Application.Responses;
public class OLWorkListResponse : OLPaginationResponse
{
    public List<OLSearchResultItem> Docs { get; set; } = new List<OLSearchResultItem>();
}