using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.Enums;

namespace Almanime.Services.Interfaces;

public interface IAnimeService
{
  IQueryable<Anime> Get();
  Anime GetBySlug(string slug);
  IEnumerable<Anime> GetByBookmarks(string auth0ID);
  IQueryable<Anime> GetSeason(int year, ESeason season);
  Task PopulateSeason(int year, ESeason season);
  IReadOnlyCollection<AnimeDocument> Search(string animeName);
}
