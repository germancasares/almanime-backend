namespace Almanime.Kitsu.Episode.Models;

public record EpisodeThumbail
{
    public string? Original { get; init; }
    public string? Tiny { get; init; }
    public string? Small { get; init; }
    public string? Large { get; init; }
}
