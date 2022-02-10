using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils;

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

    public async Task<Subtitle> Create(SubtitleDTO subtitleDTO, string? auth0ID)
    {
        if (
            subtitleDTO.FansubAcronym == null ||
            subtitleDTO.AnimeSlug == null ||
            subtitleDTO.File == null
        ) throw new ArgumentNullException(nameof(subtitleDTO));

        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));
        var user = _context.Users.GetByAuth0ID(auth0ID);
        if (user == null) throw new ArgumentNullException(nameof(auth0ID));

        var episode = _context.Episodes.GetByAnimeSlugAndNumber(subtitleDTO.AnimeSlug, subtitleDTO.EpisodeNumber);
        if (episode == null) throw new ArgumentNullException(nameof(subtitleDTO));

        var fansub = _context.Fansubs.GetByAcronym(subtitleDTO.FansubAcronym);
        if (fansub == null) throw new ArgumentNullException(nameof(subtitleDTO));

        //var member = _context.Members.GetByFansubAndUser(fansub.ID, user.ID);
        //if (member == null) throw new ArgumentNullException(nameof(subtitleDTO));


        var subtitleID = Guid.NewGuid();

        var url = await _fileService.UploadSubtitle(subtitleDTO.File, fansub.ID, subtitleID);
        if (url == null) throw new ArgumentNullException(nameof(subtitleDTO));

        var subtitle = _context.Subtitles.Add(new (
            id: subtitleID,
            status: ESubtitleStatus.Published,
            format: subtitleDTO.File.GetSubtitleFormat(),
            url: url,
            episodeID: episode.ID
            //memberID: member.ID
        ));

        _context.SaveChanges();

        return subtitle.Entity;
    }

}
