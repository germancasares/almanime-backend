namespace Almanime.Kitsu.Anime;

public record AnimeCoverImageModel
{
    public string? Original { get; init; }
    public string? Tiny { get; init; }
    public string? Small { get; init; }
    public string? Large { get; init; }
}
