namespace Almanime.ETL.Kitsu.Episode.Models;

public record EpisodeAttributesModel
{
  public string? CanonicalTitle { get; set; }
  public int? SeasonNumber { get; init; }
  public int? Number { get; init; }
  public int? RelativeNumber { get; init; }
  public string? Synopsis { get; init; }
  public string? Airdate { get; init; }
  public int? Length { get; init; }
  public EpisodeThumbail Thumbnail { get; init; } = new();
}
