using Almanime.Models;
using Almanime.Models.Enums;

namespace Almanime.Models.DTO;

public record AnimeDTO
{
    public int KitsuID { get; set; }
    public string? Slug { get; set; }
    public string? Name { get; set; }
    public ESeason Season { get; set; }
    public EAnimeStatus Status { get; set; }
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public string? Synopsis { get; set; }
    public SizedImage? CoverImageUrl { get; set; }
    public SizedImage? PosterImageUrl { get; set; }
}
