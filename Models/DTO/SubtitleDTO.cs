namespace Almanime.Models.DTO;

public readonly record struct SubtitleDTO
{
    public IFormFile? File { get; init; }

    public string? FansubAcronym { get; init; }
    public string? AnimeSlug { get; init; }
    public int EpisodeNumber { get; init; }
}
