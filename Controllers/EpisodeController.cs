using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class EpisodeController : ControllerBase
{
  private readonly IEpisodeService _episodeService;

  public EpisodeController(IEpisodeService episodeService)
  {
    _episodeService = episodeService;
  }

  [HttpGet("anime/{animeSlug}")]
  public IActionResult GetByAnimeSlug(string animeSlug)
  {
    var episodes = _episodeService.GetByAnimeSlug(animeSlug);

    return Ok(episodes.Select(episode => episode.MapToView()));
  }

  [HttpPost("populate")]
  [Authorize("write:episodes")]
  public async Task<IActionResult> Populate()
  {
    await _episodeService.Populate();
    return NoContent();
  }
}
