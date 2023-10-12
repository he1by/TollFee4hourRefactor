using System.Globalization;

namespace ToolFee.Domain.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<DateTime> ToDateTimeEnumerable(this IEnumerable<string> stringEnumerable, string dateFormat)
    {
        foreach (var str in stringEnumerable)
        {
            if (DateTime.TryParseExact(str, dateFormat, null, DateTimeStyles.None, out DateTime date))
            {
                yield return date;
            }
            else
            {
                //TODO: add logging here
            }
        }
    }
}
