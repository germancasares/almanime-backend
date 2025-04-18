using Almanime.ETL.Kitsu.Anime.Models;
using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Utils;
using System.Globalization;
using System.Text.Json;

namespace Almanime.ETL.Kitsu.Anime;

public static class KitsuAnime
{
    private const int MAX_PER_PAGE = 20;
    private const string KITSU_API = "https://kitsu.io/api/edge";
    private const string ANIME_MAPPING_URL = "https://raw.githubusercontent.com/manami-project/anime-offline-database/master/anime-offline-database-minified.json";
    private static readonly HttpClient Client = new();

    public static async Task<List<AnimeDTO>> FetchSeason(int year, ESeason season)
    {
        var mapping = await GetAnimeMapping();

        var rawAnimes = await GetRawAnimes(year, season);

        var rawAnimesFiltered = rawAnimes.Where(model => model.Id != null && IsProcessable(model.Attributes));

#pragma warning disable CS8604 // Possible null reference argument.
        var animesDTO = rawAnimesFiltered.Select(model =>
        {
            var ids = mapping.TryGetValue(model.Id, out var value) ? value : new Dictionary<string, int?>();
            return MapToDTO(model.Id, model.Attributes, ids);
        });
#pragma warning restore CS8604 // Possible null reference argument.

        var animesDTOFiltered = animesDTO.Where(a => a.Status != EAnimeStatus.Tba);

        return animesDTOFiltered.ToList();
    }

    private static async Task<List<AnimeDataModel>> GetRawAnimes(int year, ESeason season)
    {
        var animeDataModels = new List<AnimeDataModel>();
        var url = $"{KITSU_API}/anime?filter[seasonYear]={year}&filter[season]={season.ToString().ToLower(CultureInfo.CurrentCulture)}&page[limit]={MAX_PER_PAGE}";

        while (!string.IsNullOrWhiteSpace(url))
        {
            var response = await Client.GetStringAsync(url);
            var animeCollection = JsonSerializer.Deserialize<AnimeCollection>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, });

            animeDataModels.AddRange(animeCollection?.Data ?? new List<AnimeDataModel>());
            url = animeCollection?.Links.Next;
        }

        return animeDataModels;
    }

    private static async Task<Dictionary<string, Dictionary<string, int?>>> GetAnimeMapping()
    {
        var response = await Client.GetStringAsync(ANIME_MAPPING_URL);
        var animeMapping = JsonSerializer.Deserialize<Manami>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, });

        var mapping = new Dictionary<string, Dictionary<string, int?>>();

        int? getID(List<string> sources, string url)
        {
            var id = sources.LastOrDefault(source => source.Contains(url))?.Replace(url, "");
            return id == null ? null : int.Parse(id);
        }

        foreach (var manamiEntry in animeMapping?.Data ?? new List<ManamiAnime>())
        {
            if (manamiEntry.Type != "TV") continue;

            var kitsuUrl = manamiEntry.Sources.LastOrDefault(source => source.Contains("https://kitsu.io/anime/"));

            if (kitsuUrl == null) continue;

            mapping.Add(
              kitsuUrl.Replace("https://kitsu.io/anime/", ""),
              new Dictionary<string, int?>
              {
          {
            "AniDB", getID(manamiEntry.Sources, "https://anidb.net/anime/")
          },
          {
            "AniList", getID(manamiEntry.Sources, "https://anilist.co/anime/")
          },
          {
            "MyAnimeList", getID(manamiEntry.Sources, "https://myanimelist.net/anime/")
          },
              }
            );
        }

        return mapping;
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
        return Utils.DateTimeOrDefault(anime.StartDate) != null;
    }

    private static AnimeDTO MapToDTO(string? kitsuId, AnimeAttributesModel anime, Dictionary<string, int?> ids)
    {
        var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status ?? ""));
        if (!status.HasValue) throw new Exception("The EAnimeStatus could not be parsed.");

        DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate);

        return new AnimeDTO
        {
            KitsuID = int.Parse(kitsuId ?? ""),
            AniDBID = ids.TryGetValue("AniDB", out var aniDBID) ? aniDBID : null,
            AniListID = ids.TryGetValue("AniList", out var aniListID) ? aniListID : null,
            MyAnimeListID = ids.TryGetValue("MyAnimeList", out var myAnimeListID) ? myAnimeListID : null,

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
