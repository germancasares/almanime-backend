namespace Almanime.Kitsu.Anime;

public record AnimePosterImageModel
{
    public string? Tiny { get; init; }
    public string? Small { get; init; }
    public string? Medium { get; init; }
    public string? Large { get; init; }
    public string? Original { get; init; }
}
