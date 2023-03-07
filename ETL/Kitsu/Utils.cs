using System.Globalization;

namespace Almanime.ETL.Kitsu;

public static class Utils
{
  public static DateTime? DateTimeOrDefault(string? date) => DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate) ? parsedDate : null;
}
