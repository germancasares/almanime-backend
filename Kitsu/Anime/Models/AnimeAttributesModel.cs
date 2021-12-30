namespace Almanime.Kitsu.Anime;

public record AnimeAttributesModel
{
    public string? Slug { get; init; }
    public string? CanonicalTitle { get; init; }
    public string? Synopsis { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Status { get; init; }
    public string? Subtype { get; init; }
    public AnimeCoverImageModel CoverImage { get; init; } = new();
    public AnimePosterImageModel PosterImage { get; init; } = new();
}
