using Almanime.Kitsu.Episode.Models;
using Almanime.Models.DTO;
using System.Globalization;
using System.Text.Json;

namespace Almanime.Kitsu.Episode;

public static class KitsuEpisodes
{
    private const int MAX_PER_PAGE = 20;
    private const string KITSU_API = "https://kitsu.io/api/edge";
    private static readonly HttpClient Client = new();

    public static async Task<List<EpisodeDTO>> Fetch(int kitsuId)
    {
        var rawEpisodes = await GetRawEpisodes(kitsuId);

        var episodesDTO = rawEpisodes.Select(model => MapToDTO(model.Attributes));

        return episodesDTO.ToList();
    }

    private static async Task<List<EpisodeDataModel>> GetRawEpisodes(int kitsuId)
    {
        var episodeDataModels = new List<EpisodeDataModel>();
        var url = $"{KITSU_API}/anime/{kitsuId}/episodes?page[limit]={MAX_PER_PAGE}";

        while (!string.IsNullOrWhiteSpace(url))
        {
            var response = await Client.GetStringAsync(url);
            var episodeCollection = JsonSerializer.Deserialize<EpisodeCollection>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, });

            episodeDataModels.AddRange(episodeCollection?.Data ?? new List<EpisodeDataModel>());
            url = episodeCollection?.Links.Next;
        }

        return episodeDataModels;
    }

    private static EpisodeDTO MapToDTO(EpisodeAttributesModel episode) => new()
    {
        Aired = Utils.DateTimeOrDefault(episode.Airdate),
        Duration = episode.Length,
        Name = episode.CanonicalTitle,
        Number = episode.Number ?? 0,
    };
}
