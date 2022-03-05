using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SubtitleController : ControllerBase
{
    private readonly ISubtitleService _subtitleService;

    public SubtitleController(ISubtitleService subtitleService)
    {
        _subtitleService = subtitleService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostAsync([FromForm]SubtitleDTO subtitleDTO)
    {
        if (subtitleDTO.FansubAcronym == null) throw new AlmNullException(nameof(subtitleDTO.FansubAcronym));
        if (subtitleDTO.AnimeSlug == null) throw new AlmNullException(nameof(subtitleDTO.AnimeSlug));
        if (subtitleDTO.File == null) throw new AlmNullException(nameof(subtitleDTO.File));

        var subtitle = await _subtitleService.Create(
            User.GetAuth0ID(),
            subtitleDTO.FansubAcronym,
            subtitleDTO.AnimeSlug,
            subtitleDTO.EpisodeNumber,
            subtitleDTO.File
        );

        return Ok(new {
            subtitle.Url,
            subtitle.Format,
            subtitle.CreationDate,
            User = subtitle.Membership.User.Name,
            Anime = subtitle.Episode.Anime.Name,
            Episode = subtitle.Episode.Number,
        });
    }
}
