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

  public Dictionary<int, Dictionary<string, string>> GetFansubs(string animeSlug) => new(
    GetByAnimeSlug(animeSlug).Select(episode => new KeyValuePair<int, Dictionary<string, string>>(
      episode.Number,
      new Dictionary<string, string>(episode.Subtitles.Select(subtitle => new KeyValuePair<string, string>(
        subtitle.Membership.FansubRole.Fansub.Acronym,
        subtitle.Url
        )).ToList())
      )).ToList()
    );

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
    var animes = _context.Animes.Select(anime => new { anime.KitsuID, anime.ID }).ToList();

    var tasks = animes.Select(async anime => new {
      anime.ID,
      anime.KitsuID,
      Episodes = await KitsuEpisodes.Fetch(anime.KitsuID),
    });

    var algo = await Task.WhenAll(tasks);

    algo.ToList().ForEach(anime =>
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
