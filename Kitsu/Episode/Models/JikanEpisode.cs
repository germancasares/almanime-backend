namespace Almanime.Kitsu.Episode.Models;

public record JikanEpisode
{
  public EpisodeData? Data { get; init; }

  public record EpisodeData
  {
    public string? Title { get; init; }
  }
}
