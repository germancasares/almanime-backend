using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Utils;
using System.Globalization;
using System.Text.Json;

namespace Almanime.Kitsu.Anime;

public class KitsuAnime
{
    private const int MAX_PER_PAGE = 20;
    private const string KITSU_API = "https://kitsu.io/api/edge";
    private static readonly HttpClient Client = new();

    public static async Task<List<AnimeDTO>> FetchSeason(int year, ESeason season)
    {
        var rawAnimes = await GetRawAnimes(year, season);

        var rawAnimesFiltered = rawAnimes.Where(model => model.Id != null && IsProcessable(model.Attributes));

        var animesDTO = rawAnimesFiltered.Select(model => MapToDTO(model.Id, model.Attributes));

        var animesDTOFiltered = animesDTO.Where(a => a.Status != EAnimeStatus.Tba);

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

            animeDataModels.AddRange(animeCollection?.Data ?? new List<AnimeDataModel>());
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

        // Subtype
        if (anime.Subtype != "TV")
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
        if (Utils.DateTimeOrDefault(anime.StartDate) == null)
        {
            return false;
        }

        return true;
    }

    private static AnimeDTO MapToDTO(string? kitsuId, AnimeAttributesModel anime)
    {
        var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status ?? ""));
        if (!status.HasValue) throw new Exception("The EAnimeStatus could not be parsed.");

        DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate);

        return new AnimeDTO
        {
            KitsuID = int.Parse(kitsuId ?? ""),
            Slug = anime.Slug,
            Name = anime.CanonicalTitle,
            Synopsis = anime.Synopsis,
            Status = status.Value,
            StartDate = startDate,
            EndDate = Utils.DateTimeOrDefault(anime.EndDate),
            Season = EnumHelper.GetSeason(startDate.Month),
            CoverImageUrl = new SizedImage
            (
                tiny: Uri.TryCreate(anime.CoverImage?.Tiny, new UriCreationOptions(), out var coverTiny) ? coverTiny : null,
                small: Uri.TryCreate(anime.CoverImage?.Small, new UriCreationOptions(), out var coverSmall) ? coverSmall : null,
                original: Uri.TryCreate(anime.CoverImage?.Original, new UriCreationOptions(), out var coverOriginal) ? coverOriginal : null
            ),
            PosterImageUrl = new SizedImage
            (
                tiny: Uri.TryCreate(anime.PosterImage?.Tiny, new UriCreationOptions(), out var posterTiny) ? posterTiny : null,
                small: Uri.TryCreate(anime.PosterImage?.Small, new UriCreationOptions(), out var posterSmall) ? posterSmall : null,
                original: Uri.TryCreate(anime.PosterImage?.Original, new UriCreationOptions(), out var posterOriginal) ? posterOriginal : null
            ),
        };
    }
}
