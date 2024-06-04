namespace ECommerce.Shared.Common.Helper;

public static class HashCodeHelper
{
    public static int GetListHashCode<T>(IEnumerable<T> list, bool ignoreOrder = false)
    {
        int typeOfTHashCode = list.GetType().GetGenericArguments()[0].ToString().GetHashCode();

        var hashCodesOfList = list.Select(x => x != null ? x.GetHashCode() : 0).ToList();

        if (ignoreOrder)
        {
            hashCodesOfList.Sort();
        }

        return hashCodesOfList.Aggregate(typeOfTHashCode, (current, item) => current * 23 + item);
    }
}