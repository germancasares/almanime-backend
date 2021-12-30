using Almanime.Kitsu.Episode;
using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;

namespace Almanime.Services;

public class EpisodeService : IEpisodeService
{
    private readonly AlmanimeContext _context;

    public EpisodeService(AlmanimeContext context)
    {
        _context = context;
    }

    public IQueryable<Episode> GetByAnimeSlug(string animeSlug) => _context.Episodes.GetByAnimeSlug(animeSlug);

    private Episode? Create(EpisodeDTO episodeDTO, Guid animeId)
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

    public async Task Populate(string animeSlug)
    {
        var anime = _context.Animes.GetBySlug(animeSlug);

        if (anime == null) return;

        var episodesDTO = await KitsuEpisodes.Fetch(anime.KitsuID);

        episodesDTO.ForEach(episode =>
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
    }
}
