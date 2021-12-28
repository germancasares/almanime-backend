namespace Almanime.Kitsu.Anime;

public record AnimeCoverImageModel
{
    public string? Original { get; set; }
    public string? Tiny { get; set; }
    public string? Small { get; set; }
    public string? Large { get; set; }
}