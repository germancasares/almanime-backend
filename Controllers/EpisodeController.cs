using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;
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

    [HttpGet("anime/{animeSlug}/fansubs")]
    public IActionResult GetFansubs(string animeSlug)
    {
        //var fansubs = _episodeService.GetFansubs(animeSlug);

        //return Ok(fansubs);

        return Ok();
    }

    [HttpPost("populate")]
    public async Task<IActionResult> Populate()
    {
        await _episodeService.Populate();
        return Ok();
    }
}
