using Almanime.Models;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using SubtitlesParser.Classes.Parsers;
using System.Text;

namespace Almanime.Services;

public class SubtitleService : ISubtitleService
{
  private readonly AlmanimeContext _context;
  private readonly IFileService _fileService;

  public SubtitleService(
    AlmanimeContext context,
    IFileService fileService
  )
  {
    _context = context;
    _fileService = fileService;
  }

  public IQueryable<Subtitle> GetByAnimeSlug(string animeSlug) => _context.Subtitles.GetByAnimeSlugAndByPublished(animeSlug);

  public Subtitle Publish(
    string auth0ID,
    string fansubAcronym,
    string animeSlug,
    int episodeNumber
  )
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var episode = _context.Episodes.GetByAnimeSlugAndNumber(animeSlug, episodeNumber);
    var fansub = _context.Fansubs.GetByAcronym(fansubAcronym);
    var subtitle = _context.Subtitles.GetByFansubIDAndEpisodeID(fansub.ID, episode.ID);

    _context.Memberships.ThrowIfUserDoesntHavePermissionInFansub(fansub, user, EPermission.PublishSubtitle);

    if (subtitle == null) throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(subtitle), new()
    {
      { nameof(fansub.ID), fansub.ID },
      { nameof(episode.ID), episode.ID },
      { nameof(user.ID), user.ID },
    });

    subtitle.Status = ESubtitleStatus.Published;
    subtitle.ModificationDate = DateTime.Now;

    _context.SaveChanges();

    return subtitle;
  }

  public Subtitle Unpublish(
    string auth0ID,
    string fansubAcronym,
    string animeSlug,
    int episodeNumber
  )
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var episode = _context.Episodes.GetByAnimeSlugAndNumber(animeSlug, episodeNumber);
    var fansub = _context.Fansubs.GetByAcronym(fansubAcronym);
    var subtitle = _context.Subtitles.GetByFansubIDAndEpisodeID(fansub.ID, episode.ID);

    _context.Memberships.ThrowIfUserDoesntHavePermissionInFansub(fansub, user, EPermission.UnpublishSubtitle);

    if (subtitle == null) throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(subtitle), new()
    {
      { nameof(fansub.ID), fansub.ID },
      { nameof(episode.ID), episode.ID },
      { nameof(user.ID), user.ID },
    });

    subtitle.Status = ESubtitleStatus.Draft;
    subtitle.ModificationDate = DateTime.Now;

    _context.SaveChanges();

    return subtitle;
  }

  public async Task<Subtitle> CreateOrUpdate(
    string auth0ID,
    string fansubAcronym,
    string animeSlug,
    int episodeNumber,
    ESubtitleLanguage language,
    IFormFile file
  )
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var episode = _context.Episodes.GetByAnimeSlugAndNumber(animeSlug, episodeNumber);
    var fansub = _context.Fansubs.GetByAcronym(fansubAcronym);
    var membership = _context.Memberships.GetByFansubAndUser(fansub.ID, user.ID);
    var subtitle = _context.Subtitles.GetByFansubIDAndEpisodeID(fansub.ID, episode.ID);

    _context.Memberships.ThrowIfUserDoesntHavePermissionInFansub(fansub, user, EPermission.DraftSubtitle);

    var format = file.GetSubtitleFormat();

    try
    {
      if (format == ESubtitleFormat.ASS)
      {
        new SsaParser().ParseStream(file.OpenReadStream(), Encoding.Default);
      }
      // TODO
      // else if (file.GetSubtitleFormat() == ESubtitleFormat.SRT)
      // {
      //   new SrtParser().ParseStream(file.OpenReadStream(), Encoding.Default);
      // }
    }
    catch (AlmDbException)
    {
      throw new AlmDbException(EValidationCode.FormatNotValid, nameof(file), new()
      {
        { nameof(fansub.ID), fansub.ID },
        { nameof(episode.ID), episode.ID },
        { nameof(user.ID), user.ID },
      });
    }

    await _fileService.UploadSubtitle(file, fansubAcronym, animeSlug, episodeNumber, episode.Anime.Name, file.GetSubtitleFormat());

    if (subtitle == null)
    {
      subtitle = _context.Subtitles.Add(new(
        id: Guid.NewGuid(),
        status: ESubtitleStatus.Draft,
        language: language,
        format: file.GetSubtitleFormat(),
        url: $"/subtitle/fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}",
        episodeID: episode.ID,
        membershipID: membership.ID
      )).Entity;
    }
    else
    {
      subtitle.Format = file.GetSubtitleFormat();
      subtitle.ModificationDate = DateTime.Now;

      _context.Subtitles.Update(subtitle);
    }

    _context.SaveChanges();

    return subtitle;
  }

}
