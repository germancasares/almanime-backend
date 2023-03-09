using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface ISubtitleService
{
  IQueryable<Subtitle> GetByAnimeSlug(string animeSlug);

  Subtitle Publish(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber);
  Subtitle Unpublish(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber);

  Task<Subtitle> CreateOrUpdate(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber, IFormFile file);
}
