using Almanime.Models;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Domain.Enums;

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

    public async Task<Subtitle> Create(
        string auth0ID,
        string fansubAcronym,
        string animeSlug,
        int episodeNumber,
        IFormFile file
    )
    {
        var user = _context.Users.GetByAuth0ID(auth0ID);
        var episode = _context.Episodes.GetByAnimeSlugAndNumber(animeSlug, episodeNumber);
        var fansub = _context.Fansubs.GetByAcronym(fansubAcronym);
        var membership = _context.Memberships.GetByFansubAndUser(fansub.ID, user.ID);

        var hasPermissionToCreate = _context.Memberships.HasUserPermissionInFansub(fansub.ID, user.ID, EPermission.CreateSubtitle);
        if (!hasPermissionToCreate) throw new AlmPermissionException(EPermission.CreateSubtitle, user.Name, fansub.Name);

        await _fileService.UploadSubtitle(file, fansubAcronym, animeSlug, episodeNumber, episode.Anime.Name, file.GetSubtitleFormat());

        var subtitle = _context.Subtitles.Add(new(
            id: Guid.NewGuid(),
            status: ESubtitleStatus.Published,
            format: file.GetSubtitleFormat(),
            url: $"/subtitle/fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}",
            episodeID: episode.ID,
            membershipID: membership.ID
        ));

        _context.SaveChanges();

        return subtitle.Entity;
    }

}
