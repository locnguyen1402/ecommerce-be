namespace ECommerce.Shared.Common.Extensions;

public static class QueryExtensions
{
    public static List<Guid>? ToQueryGuidList(this string[]? raw)
    {
        if (raw == null)
        {
            return null;
        }

        return raw.Where(id => Guid.TryParse(id, out _)).Select(Guid.Parse).ToList();
    }
}