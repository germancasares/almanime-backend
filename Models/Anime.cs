using Almanime.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Almanime.Models;

public class Anime : Base
{
    public Anime(
        int kitsuID,
        string slug,
        string name,
        ESeason season,
        EAnimeStatus status,
        DateTime startDate,
        DateTime? endDate,
        string? synopsis,
        string? coverImageUrl,
        string? posterImageUrl
    )
    {
        KitsuID = kitsuID;
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Season = season;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        Synopsis = synopsis;
        CoverImageUrl = coverImageUrl;
        PosterImageUrl = posterImageUrl;
    }

    [Required]
    public int KitsuID { get; set; }

    [Required]
    public string Slug { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public ESeason Season { get; set; }
    [Required]
    public EAnimeStatus Status { get; set; }
    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public string? Synopsis { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? PosterImageUrl { get; set; }
}
