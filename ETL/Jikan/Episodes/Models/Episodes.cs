using System.Text.Json.Serialization;

namespace Almanime.ETL.Jikan.Episodes.Models;

public record Episodes
{
    public EpisodeData[] Data { get; init; } = Array.Empty<EpisodeData>();

    public record EpisodeData
    {
        [JsonPropertyName("mal_id")]
        public int MyAnimeListID { get; init; }
        public string? Title { get; init; }
    }
}
