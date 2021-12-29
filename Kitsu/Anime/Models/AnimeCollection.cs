namespace Almanime.Kitsu.Anime;

public record AnimeCollection
{
    public List<AnimeDataModel> Data { get; set; } = new();
    public Meta Meta { get; set; } = new();
    public Links Links { get; set; } = new();
}
