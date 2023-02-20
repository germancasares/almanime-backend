using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface ISubtitleService
{
  Task<Subtitle> CreateOrUpdate(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber, IFormFile file);
}
