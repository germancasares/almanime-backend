namespace Almanime.Kitsu.Episode.Models;

public record EpisodeDataModel
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public EpisodeAttributesModel Attributes { get; set; } = new();
}
