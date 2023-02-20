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
  private readonly IFileService _fileService;

  public SubtitleController(ISubtitleService subtitleService, IFileService fileService)
  {
    _subtitleService = subtitleService;
    _fileService = fileService;
  }

  [HttpGet("fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}")]
  public async Task<IActionResult> GetAsync(string fansubAcronym, string animeSlug, int episodeNumber)
  {
    var (file, contentType, fileName) = await _fileService.DownloadSubtitle(fansubAcronym, animeSlug, episodeNumber);

    return File(file, contentType, fileName);
  }

  [HttpPost]
  [Authorize]
  public async Task<IActionResult> PostAsync([FromForm] SubtitleDTO subtitleDTO)
  {
    if (subtitleDTO.FansubAcronym == null) throw new AlmNullException(nameof(subtitleDTO.FansubAcronym));
    if (subtitleDTO.AnimeSlug == null) throw new AlmNullException(nameof(subtitleDTO.AnimeSlug));
    if (subtitleDTO.File == null) throw new AlmNullException(nameof(subtitleDTO.File));

    var subtitle = await _subtitleService.CreateOrUpdate(
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
