namespace ECommerce.Shared.Integration.Application.Responses;
public class OLInWorkBookListResponse
{
    public int Size { get; set; } = 0;
    public List<OLBook> Entries { get; set; } = new List<OLBook>();
}