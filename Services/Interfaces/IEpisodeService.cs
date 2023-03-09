using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface IEpisodeService
{
  IQueryable<Episode> GetByAnimeSlug(string animeSlug);
  Task Populate();
}
