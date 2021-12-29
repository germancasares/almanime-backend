namespace Almanime.Kitsu.Episode.Models;

public record EpisodeAttributesModel
{
    public string? CanonicalTitle { get; set; }
    public int? SeasonNumber { get; set; }
    public int? Number { get; set; }
    public int? RelativeNumber { get; set; }
    public string? Synopsis { get; set; }
    public string? Airdate { get; set; }
    public int? Length { get; set; }
    public EpisodeThumbail Thumbnail { get; set; } = new();
}
