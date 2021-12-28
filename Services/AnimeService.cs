using Almanime.Kitsu;
using Almanime.Models;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Services.Interfaces;
using API.Models.DTOs;
using API.Utils.Mappers;

namespace API.Services;

public class AnimeService : IAnimeService
{
    private readonly AlmanimeContext _context;

    public AnimeService(AlmanimeContext context)
    {
        _context = context;
    }

    public Anime? GetByKitsuID(int kitsuID) => _context.Animes.SingleOrDefault(anime => anime.KitsuID == kitsuID);

    public IQueryable<Anime> GetSeason(int year, ESeason season)
    {
        if (season == ESeason.Winter) return _context.Animes.Where(anime => anime.StartDate >= new DateTime(year, 12, 1) && anime.StartDate < new DateTime(year+1, 3, 1));

        return _context.Animes.Where(anime => anime.StartDate.Year == year && anime.Season == season);
    }

    public Anime Create(AnimeDTO animeDTO)
    {
        var anime = _context.Animes.Add(animeDTO.MapToModel());

        _context.SaveChanges();

        return anime.Entity;
    }

    public void Update(AnimeDTO animeDTO)
    {
        var anime = GetByKitsuID(animeDTO.KitsuID);

        if (anime == null) return;

        _context.Animes.Update(anime.UpdateFromDTO(animeDTO));
        _context.SaveChanges();
    }

    public async Task PopulateSeason(int year, ESeason season)
    {
        var seasonAnimesDTO = await Kitsu.FetchSeason(year, season);

        seasonAnimesDTO.ForEach(anime =>
        {
            if (GetByKitsuID(anime.KitsuID) == null)
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