using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class SubtitleQueries
{
    public static Subtitle? Get(
      this DbSet<Subtitle> subtitles, string fansubAcronym, string animeSlug, int episodeNumber
    ) => subtitles.SingleOrDefault(subtitle =>
      subtitle.Membership.FansubRole.Fansub.Acronym == fansubAcronym
      && subtitle.Episode.Anime.Slug == animeSlug
      && subtitle.Episode.Number == episodeNumber
    );

    public static Subtitle? GetByFansubIDAndEpisodeID(
      this DbSet<Subtitle> subtitles,
      Guid fansubID,
      Guid episodeID
    ) => subtitles.SingleOrDefault(subtitle => subtitle.Membership.FansubID == fansubID && subtitle.EpisodeID == episodeID);

    public static IQueryable<Subtitle> GetByAnimeSlugAndByPublished(
      this DbSet<Subtitle> subtitles,
      string animeSlug
    ) => subtitles.Where(subtitle => subtitle.Episode.Anime.Slug == animeSlug && subtitle.Status == ESubtitleStatus.Published);
}