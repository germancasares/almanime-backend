namespace Almanime.Kitsu.Anime;

public record AnimeCollection
{
    public List<AnimeDataModel> Data { get; init; } = new();
    public Meta Meta { get; init; } = new();
    public Links Links { get; init; } = new();
}
