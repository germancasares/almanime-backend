namespace Almanime.Models.DTO;

public record SubtitleDTO
{
    public IFormFile? File { get; set; }

    public string? FansubAcronym { get; set; }
    public string? AnimeSlug { get; set; }
    public int EpisodeNumber { get; set; }
}
