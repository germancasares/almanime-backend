using Almanime.Models.Enums;

namespace Almanime.Models.Views;

public record AnimeView
{
    public Guid ID { get; init; }

    public int KitsuID { get; init; }
    public string? Slug { get; init; }
    public string? Name { get; init; }
    public ESeason Season { get; init; }
    public EAnimeStatus Status { get; init; }
    public string? Synopsis { get; init; }
    public DateTime StartDate { get; init; }

    public SizedImage? CoverImages { get; init; }
    public SizedImage? PosterImages { get; init; }
}
