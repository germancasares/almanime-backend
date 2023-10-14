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

  [HttpGet("anime/{animeSlug}")]
  public IActionResult GetByAnime(string animeSlug)
  {
    var subtitles = _subtitleService.GetByAnimeSlug(animeSlug);

    var subtitleGroups = subtitles
    .GroupBy(subtitle => subtitle.Episode.Number)
    .ToDictionary(
      group => group.Key,
      group => group.Select(subtitle => new {
        subtitle.Membership.FansubRole.Fansub.Acronym,
        subtitle.Url,
        subtitle.Language,
      })
    );

    return Ok(subtitleGroups);
  }

  [HttpGet("fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}")]
  public async Task<IActionResult> GetAsync(string fansubAcronym, string animeSlug, int episodeNumber)
  {
    var (file, contentType, fileName) = await _subtitleService.GetFile(fansubAcronym, animeSlug, episodeNumber);

    return File(file, contentType, fileName);
  }

  [HttpPatch("fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}/publish")]
  [Authorize]
  public IActionResult Publish(string fansubAcronym, string animeSlug, int episodeNumber)
  {
    if (fansubAcronym == null) throw new AlmNullException(nameof(fansubAcronym));
    if (animeSlug == null) throw new AlmNullException(nameof(animeSlug));
    if (episodeNumber <= 0) throw new AlmNullException(nameof(episodeNumber));

    var subtitle = _subtitleService.Publish(User.GetAuth0ID(), fansubAcronym, animeSlug, episodeNumber);

    return Ok(new {
      subtitle.Url,
      subtitle.Format,
      subtitle.CreationDate,
      User = subtitle.Membership.User.Name,
      Anime = subtitle.Episode.Anime.Name,
      Episode = subtitle.Episode.Number,
    });
  }

  [HttpPatch("fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}/unpublish")]
  [Authorize]
  public IActionResult Unpublish(string fansubAcronym, string animeSlug, int episodeNumber)
  {
    if (fansubAcronym == null) throw new AlmNullException(nameof(fansubAcronym));
    if (animeSlug == null) throw new AlmNullException(nameof(animeSlug));
    if (episodeNumber <= 0) throw new AlmNullException(nameof(episodeNumber));

    var subtitle = _subtitleService.Unpublish(User.GetAuth0ID(), fansubAcronym, animeSlug, episodeNumber);

    return Ok(new {
      subtitle.Url,
      subtitle.Format,
      subtitle.CreationDate,
      User = subtitle.Membership.User.Name,
      Anime = subtitle.Episode.Anime.Name,
      Episode = subtitle.Episode.Number,
    });
  }

  [HttpDelete("fansub/{fansubAcronym}/anime/{animeSlug}/episode/{episodeNumber}")]
  [Authorize]
  public async Task<IActionResult> DeleteAsync(string fansubAcronym, string animeSlug, int episodeNumber)
  {
    var auth0ID = User.GetAuth0ID();

    await _subtitleService.Delete(auth0ID, fansubAcronym, animeSlug, episodeNumber);

    return NoContent();
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
      subtitleDTO.Language,
      subtitleDTO.File
    );

    return Ok(new {
      subtitle.Url,
      subtitle.Format,
      subtitle.CreationDate,
      subtitle.Language,
      User = subtitle.Membership.User.Name,
      Anime = subtitle.Episode.Anime.Name,
      Episode = subtitle.Episode.Number,
    });
  }
}
