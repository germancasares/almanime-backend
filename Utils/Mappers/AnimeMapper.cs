using Almanime.Models;
using Almanime.Models.Views;
using API.Models.DTOs;

namespace API.Utils.Mappers;

public static class AnimeMapper
{
    public static Anime MapToModel(this AnimeDTO anime)
    {
        return new Anime(
            kitsuID: anime.KitsuID,
            slug: anime.Slug ?? throw new ArgumentNullException(nameof(anime), "The value of 'anime.Slug' should not be null"),
            name: anime.Name ?? throw new ArgumentNullException(nameof(anime), "The value of 'anime.Name' should not be null"),
            season: anime.Season,
            status: anime.Status,
            startDate: anime.StartDate,
            endDate: anime.EndDate,
            synopsis: anime.Synopsis,
            coverImageUrl: anime.CoverImageUrl,
            posterImageUrl: anime.PosterImageUrl
        );
    }
    public static Anime UpdateFromDTO(this Anime anime, AnimeDTO animeDTO)
    {
        anime.KitsuID = animeDTO.KitsuID;
        anime.Slug = animeDTO.Slug ?? throw new ArgumentNullException(nameof(animeDTO), "The value of 'animeDTO.Slug' should not be null");
        anime.Name = animeDTO.Name ?? throw new ArgumentNullException(nameof(animeDTO), "The value of 'animeDTO.Name' should not be null");
        anime.Season = animeDTO.Season;
        anime.Status = animeDTO.Status;
        anime.StartDate = animeDTO.StartDate;
        anime.EndDate = animeDTO.EndDate;
        anime.Synopsis = animeDTO.Synopsis;
        anime.CoverImageUrl = animeDTO.CoverImageUrl;
        anime.PosterImageUrl = animeDTO.PosterImageUrl;

        return anime;
    }

    public static AnimeView MapToView(this Anime anime)
    {
        return new AnimeView
        {
            ID = anime.ID,
            KitsuID = anime.KitsuID,
            Slug = anime.Slug,
            Name = anime.Name,
            Season = anime.Season,
            Status = anime.Status,
            Synopsis = anime.Synopsis,
            StartDate = anime.StartDate,
            CoverImage = anime.CoverImageUrl,
            PosterImage = anime.PosterImageUrl,
        };
    }
}
