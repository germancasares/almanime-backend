using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.DTO;
using Almanime.Models.Views;

namespace Almanime.Utils.Mappers;

public static class AnimeMapper
{
  public static Anime MapToModel(this AnimeDTO anime) => new(
    kitsuID: anime.KitsuID,
    myAnimeListID: anime.MyAnimeListID,
    aniListID: anime.AniListID,
    aniDBID: anime.AniDBID,
    slug: anime.Slug ?? throw new ArgumentNullException(nameof(anime), "The value of 'anime.Slug' should not be null"),
    name: anime.Name ?? throw new ArgumentNullException(nameof(anime), "The value of 'anime.Name' should not be null"),
    season: anime.Season,
    status: anime.Status,
    startDate: anime.StartDate,
    endDate: anime.EndDate,
    synopsis: anime.Synopsis,
    coverImages: anime.CoverImageUrl,
    posterImages: anime.PosterImageUrl
  );

  public static Anime UpdateFromDTO(this Anime anime, AnimeDTO animeDTO)
  {
    anime.KitsuID = animeDTO.KitsuID;
    anime.MyAnimeListID = animeDTO.MyAnimeListID;
    anime.AniListID = animeDTO.AniListID;
    anime.AniDBID = animeDTO.AniDBID;
    anime.Slug = animeDTO.Slug ?? throw new ArgumentNullException(nameof(animeDTO), "The value of 'animeDTO.Slug' should not be null");
    anime.Name = animeDTO.Name ?? throw new ArgumentNullException(nameof(animeDTO), "The value of 'animeDTO.Name' should not be null");
    anime.Season = animeDTO.Season;
    anime.Status = animeDTO.Status;
    anime.StartDate = animeDTO.StartDate;
    anime.EndDate = animeDTO.EndDate;
    anime.Synopsis = animeDTO.Synopsis;
    anime.CoverImages = animeDTO.CoverImageUrl;
    anime.PosterImages = animeDTO.PosterImageUrl;
    anime.ModificationDate = DateTime.Now;

    return anime;
  }

  public static AnimeView MapToView(this Anime anime) => new()
  {
    ID = anime.ID,
    KitsuID = anime.KitsuID,
    MyAnimeListID = anime.MyAnimeListID,
    AniListID = anime.AniListID,
    AniDBID = anime.AniDBID,
    Slug = anime.Slug,
    Name = anime.Name,
    Season = anime.Season,
    Status = anime.Status,
    Synopsis = anime.Synopsis,
    StartDate = anime.StartDate,
    CoverImages = anime.CoverImages,
    PosterImages = anime.PosterImages,
  };

  public static AnimeDocument MapToDocument(this Anime anime) => new()
  {
    ID = anime.ID,
    CreationDate = anime.CreationDate,
    Name = anime.Name,
    Slug = anime.Slug,
    Season = anime.Season,
    StartDate = anime.StartDate
  };
}
