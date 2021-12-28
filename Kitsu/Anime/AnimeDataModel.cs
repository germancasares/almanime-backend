namespace Almanime.Kitsu.Anime;

public record AnimeDataModel
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public AnimeAttributesModel Attributes { get; set; } = new();
}