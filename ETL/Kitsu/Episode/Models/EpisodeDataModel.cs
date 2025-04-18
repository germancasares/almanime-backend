namespace Almanime.ETL.Kitsu.Episode.Models;

public record EpisodeDataModel
{
    public string? Id { get; init; }
    public string? Type { get; init; }
    public EpisodeAttributesModel Attributes { get; init; } = new();
}
