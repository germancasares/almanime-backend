using Almanime.Models.Enums;

namespace API.Models.DTOs;

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
    public string? CoverImageUrl { get; set; }
    public string? PosterImageUrl { get; set; }
}
