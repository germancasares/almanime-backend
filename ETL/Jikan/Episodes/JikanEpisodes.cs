using Almanime.Models.DTO;
using Almanime.Utils;
using System.Threading.RateLimiting;

namespace Almanime.ETL.Jikan.Episodes;

public static class JikanEpisodes
{
    private const string JIKAN_API = "https://api.jikan.moe/v4";

    private static readonly HttpClient Client = new(new RateLimitedHandler(
      new TokenBucketRateLimiter(
        new TokenBucketRateLimiterOptions
        {
            TokenLimit = 1,
            QueueLimit = int.MaxValue,
            ReplenishmentPeriod = TimeSpan.FromSeconds(2),
            TokensPerPeriod = 1,
        }
      )
    ))
    {
        Timeout = TimeSpan.FromMinutes(5),
    };

    public static async Task<List<EpisodeDTO>> Fetch(int? myAnimeListID)
    {
        if (myAnimeListID == null) return new List<EpisodeDTO>();

        var jikanEpisodes = await Client.GetFromJsonAsync<Models.Episodes>($"{JIKAN_API}/anime/{myAnimeListID}/episodes");

        return jikanEpisodes?.Data.Select(jikanEpisode => new EpisodeDTO
        {
            Name = jikanEpisode.Title,
            Number = jikanEpisode.MyAnimeListID,
        }).ToList() ?? new List<EpisodeDTO>();
    }
}