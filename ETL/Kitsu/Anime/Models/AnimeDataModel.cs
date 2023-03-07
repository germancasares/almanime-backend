namespace Almanime.ETL.Kitsu.Anime.Models;

public record AnimeDataModel
{
  public string? Id { get; init; }
  public string? Type { get; init; }
  public AnimeAttributesModel Attributes { get; init; } = new();
}
