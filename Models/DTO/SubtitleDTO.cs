using Almanime.Models.Enums;

namespace Almanime.Models.DTO;

public record SubtitleDTO
{
    public IFormFile? File { get; init; }

    public string? FansubAcronym { get; init; }
    public string? AnimeSlug { get; init; }
    public int EpisodeNumber { get; init; }
    public ESubtitleLanguage Language { get; init; }
}
