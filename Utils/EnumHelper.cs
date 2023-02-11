using Almanime.Models.Enums;

namespace Almanime.Utils;

public static class EnumHelper
{
  public static T? GetEnumFromString<T>(string? value) where T : struct
  {
    if (value == null) return null;

    var isEnum = Enum.TryParse<T>(value, out var type);

    return isEnum ? type : null;
  }

  public static ESeason GetSeason(int month) => month switch
  {
    int n when n is >= 1 and <= 2 => ESeason.Winter,
    int n when n is >= 3 and <= 5 => ESeason.Spring,
    int n when n is >= 6 and <= 8 => ESeason.Summer,
    int n when n is >= 9 and <= 11 => ESeason.Fall,
    int n when n == 12 => ESeason.Winter,
    _ => throw new Exception("Season out of range."),
  };

  public static ESubtitleFormat GetSubtitleFormat(this IFormFile file) => file.GetExtension() switch
  {
    ".ass" => ESubtitleFormat.ASS,
    ".srt" => ESubtitleFormat.SRT,
    _ => throw new ArgumentNullException(nameof(file)),
  };
}
