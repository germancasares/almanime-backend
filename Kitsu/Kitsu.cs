using Almanime.Kitsu.Anime;
using Almanime.Models.Enums;
using Almanime.Utils;
using API.Models.DTOs;
using System.Globalization;
using System.Text.Json;

namespace Almanime.Kitsu;

public class Kitsu
{
    private const int MAX_PER_PAGE = 20;
    private const string KITSU_API = "https://kitsu.io/api/edge";
    private static readonly HttpClient Client = new();

    public static async Task<List<AnimeDTO>> FetchSeason(int year, ESeason season)
    {
        var rawAnimes = await GetRawAnimes(year, season);

        var rawAnimesFiltered = rawAnimes.Where(model => model.Id is not null && IsProcessable(model.Attributes));

        var animesDTO = rawAnimesFiltered.Select(model => MapAnime(model.Id, model.Attributes));

        var animesDTOFiltered = animesDTO.Where(a => a != null && a.Status != EAnimeStatus.Tba && a.Season == season);

        return animesDTOFiltered.ToList();
    }

    private static async Task<List<AnimeDataModel>> GetRawAnimes(int year, ESeason season)
    {
        var animeDataModels = new List<AnimeDataModel>();
        var url = $"{KITSU_API}/anime?filter[seasonYear]={year}&filter[season]={season.ToString().ToLower()}&page[limit]={MAX_PER_PAGE}";

        while (!string.IsNullOrWhiteSpace(url))
        {
            var response = await Client.GetStringAsync(url);
            var animeCollection = JsonSerializer.Deserialize<AnimeCollection>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, });

            var rawAnimes = animeCollection?.Data;
            var filteredAnimes = rawAnimes?.Where(c => c.Attributes.Subtype == "TV").ToList();

            animeDataModels.AddRange(filteredAnimes ?? new List<AnimeDataModel>());
            url = animeCollection?.Links.Next;
        }

        return animeDataModels;
    }

    private static bool IsProcessable(AnimeAttributesModel anime)
    {
        // Anime
        if (anime is null)
        {
            return false;
        }

        // Slug
        if (anime.Slug is "delete")
        {
            return false;
        }

        // Status
        var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status ?? ""));
        if (!status.HasValue)
        {
            return false;
        }

        // StartDate
        if (!DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return false;
        }

        return true;
    }

    private static string? GetBaseUrl(string? id, string? url)
    {
        if (id is null || url is null) return null;
        return url[..(url.IndexOf(id) + id.Length + 1)];
    }

    private static AnimeDTO MapAnime(string id, AnimeAttributesModel anime)
    {
        var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status ?? ""));
        if (!status.HasValue) throw new Exception("The EAnimeStatus could not be parsed.");

        DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
        var endDateCorrect = DateTime.TryParseExact(anime.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

        return new AnimeDTO
        {
            KitsuID = int.Parse(id),
            Slug = anime.Slug,
            Name = anime.CanonicalTitle,
            Synopsis = anime.Synopsis,
            Status = status.Value,
            StartDate = startDate,
            EndDate = endDateCorrect ? endDate : null,
            Season = EnumHelper.GetSeason(startDate.Month),
            CoverImageUrl = anime.CoverImage?.Tiny,
            PosterImageUrl = anime.PosterImage?.Tiny,
        };
    }
}
