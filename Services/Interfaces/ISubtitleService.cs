using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface ISubtitleService
{
    Task<Subtitle> Create(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber, IFormFile file);
}
