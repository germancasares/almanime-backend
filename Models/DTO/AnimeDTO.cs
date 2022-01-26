using Almanime.Models.Enums;

namespace Almanime.Models.DTO;

public readonly record struct AnimeDTO
{
    public int KitsuID { get; init; }
    public string? Slug { get; init; }
    public string? Name { get; init; }
    public ESeason Season { get; init; }
    public EAnimeStatus Status { get; init; }
    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }
    public string? Synopsis { get; init; }
    public SizedImage? CoverImageUrl { get; init; }
    public SizedImage? PosterImageUrl { get; init; }
}
