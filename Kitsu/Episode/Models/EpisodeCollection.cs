namespace Almanime.Kitsu.Episode.Models;

public record EpisodeCollection
{
    public List<EpisodeDataModel> Data { get; set; } = new();
    public Meta Meta { get; set; } = new();
    public Links Links { get; set; } = new();
}
