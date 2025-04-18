namespace Almanime.ETL.Kitsu.Episode.Models;

public record EpisodeCollection
{
    public List<EpisodeDataModel> Data { get; init; } = new();
    public Meta Meta { get; init; } = new();
    public Links Links { get; init; } = new();
}
