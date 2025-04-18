using Almanime.Models.Enums;

namespace Almanime.Models.DTO;

public record AnimeDTO
{
    public int KitsuID { get; init; }
    public int? MyAnimeListID { get; init; }
    public int? AniListID { get; init; }
    public int? AniDBID { get; init; }
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
