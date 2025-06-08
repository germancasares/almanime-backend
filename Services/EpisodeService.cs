using Almanime.ETL.Jikan.Episodes;
using Almanime.ETL.Kitsu.Episode;
using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;

namespace Almanime.Services;

public class EpisodeService(AlmanimeContext context) : IEpisodeService
{
    private readonly AlmanimeContext _context = context;

    public IQueryable<Episode> GetByAnimeSlug(string animeSlug) => _context.Episodes.GetByAnimeSlug(animeSlug);

    private Episode Create(EpisodeDTO episodeDTO, Guid animeId)
    {
        var episode = _context.Episodes.Add(episodeDTO.MapToModel(animeId));

        _context.SaveChanges();

        return episode.Entity;
    }

    private void Update(EpisodeDTO episodeDTO, int kitsuId)
    {
        var episode = _context.Episodes.GetByKitsuIdAndNumber(kitsuId, episodeDTO.Number);
        if (episode == null) return;

        _context.Episodes.Update(episode.UpdateFromDTO(episodeDTO));
        _context.SaveChanges();
    }

    public async Task Populate()
    {
        var animes = _context.Animes.Select(anime => new { anime.KitsuID, anime.ID, anime.MyAnimeListID }).ToList();

        var tasks = animes.Select(async anime =>
        {
            var episodes = await KitsuEpisodes.Fetch(anime.KitsuID);

            if (episodes.Any(episode => episode.Name == null))
            {
                var jikanEpisodes = await JikanEpisodes.Fetch(anime.MyAnimeListID);

                episodes = episodes.Select(episode =>
            {
                episode.Name ??= jikanEpisodes.SingleOrDefault(jikanEpisode => jikanEpisode.Number == episode.Number)?.Name;

                return episode;
            }).ToList();
            }

            return new
            {
                anime.ID,
                anime.KitsuID,
                Episodes = episodes,
            };
        });

        (await Task.WhenAll(tasks)).ToList().ForEach(anime =>
        {
            anime.Episodes.ForEach(episode =>
          {
                if (_context.Episodes.GetByKitsuIdAndNumber(anime.KitsuID, episode.Number) == null)
                {
                    Create(episode, anime.ID);
                }
                else
                {
                    Update(episode, anime.KitsuID);
                }
            });
        });
    }
}
