using Almanime.Kitsu.Anime;
using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;

namespace Almanime.Services;

public class AnimeService : IAnimeService
{
    private readonly AlmanimeContext _context;

    public AnimeService(AlmanimeContext context)
    {
        _context = context;
    }

    public Anime? GetBySlug(string slug) => _context.Animes.GetBySlug(slug);

    public IQueryable<Anime> GetSeason(int year, ESeason season)
    {
        if (season == ESeason.Winter) return _context.Animes.Where(anime => anime.Season == season && (anime.StartDate.Month >= 12 || anime.StartDate.Month < 3));

        return _context.Animes.Where(anime => anime.StartDate.Year == year && anime.Season == season);
    }

    private Anime Create(AnimeDTO animeDTO)
    {
        var anime = _context.Animes.Add(animeDTO.MapToModel());

        _context.SaveChanges();

        return anime.Entity;
    }

    private void Update(AnimeDTO animeDTO)
    {
        var anime = _context.Animes.GetByKitsuID(animeDTO.KitsuID);

        if (anime == null) return;

        _context.Animes.Update(anime.UpdateFromDTO(animeDTO));
        _context.SaveChanges();
    }

    public async Task PopulateSeason(int year, ESeason season)
    {
        var seasonAnimesDTO = await KitsuAnime.FetchSeason(year, season);

        seasonAnimesDTO.ForEach(anime =>
        {
            if (_context.Animes.GetByKitsuID(anime.KitsuID) == null)
            {
                Create(anime);
            }
            else
            {
                Update(anime);
            }
        });
    }
}
