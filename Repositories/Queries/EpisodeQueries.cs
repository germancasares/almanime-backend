using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class EpisodeQueries
{
  public static Episode? GetByKitsuIdAndNumber(this DbSet<Episode> episodes, int kitsuId, int number) => episodes.SingleOrDefault(episode => episode.Anime.KitsuID == kitsuId && episode.Number == number);
  public static Episode GetByAnimeSlugAndNumber(this DbSet<Episode> episodes, string animeSlug, int number)
  {
    var episode = episodes.SingleOrDefault(episode => episode.Anime.Slug == animeSlug && episode.Number == number);
    return episode ?? throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(episode), new()
    {
      { nameof(animeSlug), animeSlug },
      { nameof(number), number }
    });
  }

  public static IQueryable<Episode> GetByAnimeSlug(this DbSet<Episode> episodes, string animeSlug) => episodes.Where(episode => episode.Anime.Slug == animeSlug);
}
